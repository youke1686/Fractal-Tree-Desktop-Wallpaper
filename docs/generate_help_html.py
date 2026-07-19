#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
生成多语言帮助文档HTML文件
将所有语言的 md 文件合并成一个 HTML，通过下拉框切换语言
"""

import re
import os
from pathlib import Path

# 语言配置
LANGUAGES = {
    'zh': {'name': '中文', 'file': 'help.md', 'lang': 'zh-CN', 'title': '分形树桌面壁纸 帮助'},
    'en': {'name': 'English', 'file': 'help_en.md', 'lang': 'en', 'title': 'Fractal Tree Desktop Wallpaper - Help'},
    'ja': {'name': '日本語', 'file': 'help_ja.md', 'lang': 'ja', 'title': 'フラクタルツリー デスクトップ壁紙 - ヘルプ'},
    'ko': {'name': '한국어', 'file': 'help_ko.md', 'lang': 'ko', 'title': '프랙탈 트리 데스크톱 월페이퍼 - 도움말'},
    'es': {'name': 'Español', 'file': 'help_es.md', 'lang': 'es', 'title': 'Fractal Tree Fondos de Escritorio - Ayuda'},
    'de': {'name': 'Deutsch', 'file': 'help_de.md', 'lang': 'de', 'title': 'Fraktalbaum Desktop-Hintergrundbild - Hilfe'},
    'fr': {'name': 'Français', 'file': 'help_fr.md', 'lang': 'fr', 'title': 'Arbre Fractal Fond d\'Écran - Aide'},
    'ru': {'name': 'Русский', 'file': 'help_ru.md', 'lang': 'ru', 'title': 'Фрактальное Дерево Обои - Справка'},
}

SCRIPT_DIR = Path(__file__).parent


def parse_markdown(content: str) -> str:
    """将 Markdown 转换为 HTML（简化版，处理常用元素）"""
    lines = content.split('\n')
    html_parts = []
    in_code_block = False
    code_content = []
    in_list = False
    list_items = []

    i = 0
    while i < len(lines):
        line = lines[i]

        # 柱状图标记处理
        if '<!-- BAR_CHART:' in line:
            # 解析柱状图数据
            match = re.search(r'<!-- BAR_CHART:\s*\[([^\]]+)\]\s*\|\s*\[([^\]]+)\]\s*-->', line)
            if match:
                values_str = match.group(1)
                labels_str = match.group(2)
                # 解析数值
                values = [float(v.strip().replace(',', '.')) for v in values_str.split(',')]
                labels = [l.strip() for l in labels_str.split(',')]
                html_parts.append(get_bar_chart_html(values, labels))
            i += 1
            continue

        # 代码块处理
        if line.strip().startswith('```'):
            if in_code_block:
                # 结束代码块
                in_code_block = False
                code_content = []
            else:
                in_code_block = True
                code_content = []
            i += 1
            continue

        if in_code_block:
            code_content.append(line)
            i += 1
            continue

        # 列表处理
        stripped = line.strip()
        if stripped.startswith('- ') or stripped.startswith('* '):
            if not in_list:
                in_list = True
                list_items = []
            list_items.append(stripped[2:])
            i += 1
            continue
        elif in_list and stripped == '':
            # 列表结束
            html_parts.append('<ul>')
            for item in list_items:
                html_parts.append('<li>' + process_inline(item) + '</li>')
            html_parts.append('</ul>')
            in_list = False
            list_items = []

        # 空行
        if stripped == '':
            html_parts.append('')
            i += 1
            continue

        # 标题
        if line.startswith('####'):
            html_parts.append('<h4 id="' + generate_id(stripped[4:].strip()) + '">' + process_inline(stripped[4:].strip()) + '</h4>')
        elif line.startswith('###'):
            html_parts.append('<h3 id="' + generate_id(stripped[3:].strip()) + '">' + process_inline(stripped[3:].strip()) + '</h3>')
        elif line.startswith('##'):
            html_parts.append('<h2 id="' + generate_id(stripped[2:].strip()) + '">' + process_inline(stripped[2:].strip()) + '</h2>')
        elif line.startswith('#'):
            html_parts.append('<h1>' + process_inline(stripped[1:].strip()) + '</h1>')
        # 引用
        elif line.startswith('>'):
            quote_content = stripped[1:].strip()
            html_parts.append('<blockquote><p>' + process_inline(quote_content) + '</p></blockquote>')
        # 分隔线
        elif stripped == '---':
            html_parts.append('<hr>')
        # 段落（检查是否是HTML标签开头）
        elif stripped.startswith('<'):
            html_parts.append(line)
        else:
            html_parts.append('<p>' + process_inline(stripped) + '</p>')

        i += 1

    # 处理未结束的列表
    if in_list and list_items:
        html_parts.append('<ul>')
        for item in list_items:
            html_parts.append('<li>' + process_inline(item) + '</li>')
        html_parts.append('</ul>')

    return '\n'.join(html_parts)


def generate_id(text: str) -> str:
    """根据文本生成ID"""
    # 移除特殊字符
    text = re.sub(r'[^\w\s\u4e00-\u9fff\u3040-\u309f\u30a0-\u30ff\uac00-\ud7afa-zA-Z0-9-]', '', text)
    text = text.replace(' ', '-')
    return text.lower()[:50]


def process_inline(text: str) -> str:
    """处理行内元素"""
    # 处理链接 [text](url)
    text = re.sub(r'\[([^\]]+)\]\(([^)]+)\)', r'<a href="\2">\1</a>', text)
    # 处理粗体 **text**
    text = re.sub(r'\*\*([^*]+)\*\*', r'<strong>\1</strong>', text)
    # 处理斜体 *text* 或 _text_
    text = re.sub(r'\*([^*]+)\*', r'<em>\1</em>', text)
    text = re.sub(r'_([^_]+)_', r'<em>\1</em>', text)
    # 处理行内代码 `code`
    text = re.sub(r'`([^`]+)`', r'<code>\1</code>', text)
    # 处理 <br>
    text = text.replace('<br>', '<br>')
    text = text.replace('<br/>', '<br>')
    return text


def read_md_file(filepath: Path) -> str:
    """读取md文件并转换为HTML"""
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    # 处理多行数学公式（简单转换为代码块）
    content = re.sub(r'\$\$(.*?)\$\$', r'<pre><code>\1</code></pre>', content, flags=re.DOTALL)

    return parse_markdown(content)


def build_toc(lang_code: str) -> str:
    """构建目录HTML（固定结构，使用翻译后的标题）"""
    # 目录结构是固定的，只需翻译标题
    toc_translations = {
        'zh': {
            'toc_title': '目录',
            'intro': '简介',
            'params_intro': '参数入门',
            'params_advanced': '参数进阶',
            'global_settings': '全局设置',
            'tree_name': '树名',
            'bg_color': '背景颜色',
            'basic_shape': '基本形态参数',
            'trunk_length': '主干长度',
            'min_branch': '最短分支长度',
            'max_depth': '最大深度',
            'leaf_threshold': '叶子阈值',
            'max_inclination': '枝条最大倾角',
            'angle_range': '分叉角度范围',
            'angle_dist': '角度概率分布曲线',
            'angle_example': '参考示例',
            'decay_range': '长度衰减范围',
            'decay_dist': '衰减概率分布曲线',
            'decay_example': '参考示例',
            'trunk_leaf': '树干/叶片设置',
            'color': '颜色',
            'weight': '权重',
            'line_width': '线宽比',
            'fallen_leaf': '落叶系统',
            'fallen_density': '落叶密度',
            'fallen_dispersion': '落叶分散程度',
            'param_group': '参数组系统',
            'why_param_group': '为什么需要参数组',
            'basic_ops': '基本操作',
            'typical_height': '典型生效高度',
            'weight_calc': '权重计算',
            'param_example': '参考示例',
            'import_export': '导入/导出树种',
            'render_window': '渲染窗口',
        },
        'en': {
            'toc_title': 'Contents',
            'intro': 'Introduction',
            'params_intro': 'Getting Started',
            'params_advanced': 'Advanced Parameters',
            'global_settings': 'Global Settings',
            'tree_name': 'Tree Name',
            'bg_color': 'Background Color',
            'basic_shape': 'Basic Shape Parameters',
            'trunk_length': 'Trunk Length',
            'min_branch': 'Minimum Branch Length',
            'max_depth': 'Max Depth',
            'leaf_threshold': 'Leaf Threshold',
            'max_inclination': 'Maximum Branch Inclination',
            'angle_range': 'Branch Angle Range',
            'angle_dist': 'Angle Probability Distribution Curve',
            'angle_example': 'Reference Example',
            'decay_range': 'Length Decay Range',
            'decay_dist': 'Decay Probability Distribution Curve',
            'decay_example': 'Reference Example',
            'trunk_leaf': 'Trunk/Leaf Settings',
            'color': 'Color',
            'weight': 'Weight',
            'line_width': 'Line Width Ratio',
            'fallen_leaf': 'Fallen Leaf System',
            'fallen_density': 'Fallen Leaf Density',
            'fallen_dispersion': 'Fallen Leaf Dispersion',
            'param_group': 'Parameter Group System',
            'why_param_group': 'Why Parameter Groups are Needed',
            'basic_ops': 'Basic Operations',
            'typical_height': 'Typical Effective Height',
            'weight_calc': 'Weight Calculation',
            'param_example': 'Reference Example',
            'import_export': 'Import/Export Tree Species',
            'render_window': 'Render Window',
        },
        'ja': {
            'toc_title': '目次',
            'intro': 'はじめに',
            'params_intro': 'パラメータ入門',
            'params_advanced': 'パラメータ応用',
            'global_settings': 'グローバル設定',
            'tree_name': '木の名前',
            'bg_color': '背景色',
            'basic_shape': '基本形状パラメータ',
            'trunk_length': '幹の長さ',
            'min_branch': '最小枝の長さ',
            'max_depth': '最大深度',
            'leaf_threshold': '葉の閾値',
            'max_inclination': '最大枝傾斜角',
            'angle_range': '分岐角度範囲',
            'angle_dist': '角度確率分布曲線',
            'angle_example': '参考例',
            'decay_range': '長さ減衰範囲',
            'decay_dist': '減衰確率分布曲線',
            'decay_example': '参考例',
            'trunk_leaf': '幹/葉の設定',
            'color': '色',
            'weight': '重み',
            'line_width': '線幅比',
            'fallen_leaf': '落ち葉システム',
            'fallen_density': '落ち葉密度',
            'fallen_dispersion': '落ち葉の分散度',
            'param_group': 'パラメータグループシステム',
            'why_param_group': 'パラメータグループが必要な理由',
            'basic_ops': '基本操作',
            'typical_height': '典型的有効高さ',
            'weight_calc': '重み計算',
            'param_example': '参考例',
            'import_export': '木種のインポート/エクスポート',
            'render_window': 'レンダリングウィンドウ',
        },
        'ko': {
            'toc_title': '목차',
            'intro': '소개',
            'params_intro': '파라미터 입문',
            'params_advanced': '파라미터 심화',
            'global_settings': '글로벌 설정',
            'tree_name': '나무 이름',
            'bg_color': '배경색',
            'basic_shape': '기본 형태 파라미터',
            'trunk_length': '줄기 길이',
            'min_branch': '최소 가지 길이',
            'max_depth': '최대 깊이',
            'leaf_threshold': '잎 임계값',
            'max_inclination': '최대 가지 기울기',
            'angle_range': '분기 각도 범위',
            'angle_dist': '각도 확률 분포 곡선',
            'angle_example': '참고 예시',
            'decay_range': '길이 감쇠 범위',
            'decay_dist': '감쇠 확률 분포 곡선',
            'decay_example': '참고 예시',
            'trunk_leaf': '줄기/잎 설정',
            'color': '색상',
            'weight': '가중치',
            'line_width': '선 너비 비율',
            'fallen_leaf': '낙엽 시스템',
            'fallen_density': '낙엽 밀도',
            'fallen_dispersion': '낙엽 분산도',
            'param_group': '파라미터 그룹 시스템',
            'why_param_group': '파라미터 그룹이 필요한 이유',
            'basic_ops': '기본 작업',
            'typical_height': '대표적 유효 높이',
            'weight_calc': '가중치 계산',
            'param_example': '참고 예시',
            'import_export': '나무 종 가져오기/내보내기',
            'render_window': '렌더링 창',
        },
        'es': {
            'toc_title': 'Contenido',
            'intro': 'Introducción',
            'params_intro': 'Introducción a los Parámetros',
            'params_advanced': 'Parámetros Avanzados',
            'global_settings': 'Configuración Global',
            'tree_name': 'Nombre del Árbol',
            'bg_color': 'Color de Fondo',
            'basic_shape': 'Parámetros de Forma Básica',
            'trunk_length': 'Longitud del Tronco',
            'min_branch': 'Longitud Mínima de Rama',
            'max_depth': 'Profundidad Máxima',
            'leaf_threshold': 'Umbral de Hoja',
            'max_inclination': 'Inclinación Máxima de Rama',
            'angle_range': 'Rango de Ángulo de Bifurcación',
            'angle_dist': 'Curva de Distribución de Probabilidad de Ángulo',
            'angle_example': 'Ejemplo de Referencia',
            'decay_range': 'Rango de Decaimiento de Longitud',
            'decay_dist': 'Curva de Distribución de Probabilidad de Decaimiento',
            'decay_example': 'Ejemplo de Referencia',
            'trunk_leaf': 'Configuración de Tronco/Hojas',
            'color': 'Color',
            'weight': 'Peso',
            'line_width': 'Proporción de Ancho de Línea',
            'fallen_leaf': 'Sistema de Hojas Caídas',
            'fallen_density': 'Densidad de Hojas Caídas',
            'fallen_dispersion': 'Dispersión de Hojas Caídas',
            'param_group': 'Sistema de Grupos de Parámetros',
            'why_param_group': 'Por qué se Necesitan Grupos de Parámetros',
            'basic_ops': 'Operaciones Básicas',
            'typical_height': 'Altura Efectiva Típica',
            'weight_calc': 'Cálculo de Peso',
            'param_example': 'Ejemplo de Referencia',
            'import_export': 'Importar/Exportar Especies de Árboles',
            'render_window': 'Ventana de Renderizado',
        },
        'de': {
            'toc_title': 'Inhaltsverzeichnis',
            'intro': 'Einführung',
            'params_intro': 'Parameter-Einführung',
            'params_advanced': 'Fortgeschrittene Parameter',
            'global_settings': 'Globale Einstellungen',
            'tree_name': 'Baumname',
            'bg_color': 'Hintergrundfarbe',
            'basic_shape': 'Grundlegende Formparameter',
            'trunk_length': 'Stammlänge',
            'min_branch': 'Minimale Astlänge',
            'max_depth': 'Maximale Tiefe',
            'leaf_threshold': 'Blattschwellenwert',
            'max_inclination': 'Maximale Astneigung',
            'angle_range': 'Verzweigungswinkelbereich',
            'angle_dist': 'Winkelwahrscheinlichkeits-Verteilungskurve',
            'angle_example': 'Referenzbeispiel',
            'decay_range': 'Längenabnahmebereich',
            'decay_dist': 'Abnahmewahrscheinlichkeits-Verteilungskurve',
            'decay_example': 'Referenzbeispiel',
            'trunk_leaf': 'Stamm/Blatt-Einstellungen',
            'color': 'Farbe',
            'weight': 'Gewicht',
            'line_width': 'Linienbreitenverhältnis',
            'fallen_leaf': 'Fallblatt-System',
            'fallen_density': 'Fallblattdichte',
            'fallen_dispersion': 'Fallblattstreuung',
            'param_group': 'Parametergruppen-System',
            'why_param_group': 'Warum Parametergruppen benötigt werden',
            'basic_ops': 'Grundlegende Operationen',
            'typical_height': 'Typische effektive Höhe',
            'weight_calc': 'Gewichtungsberechnung',
            'param_example': 'Referenzbeispiel',
            'import_export': 'Baumarten importieren/exportieren',
            'render_window': 'Rendering-Fenster',
        },
        'fr': {
            'toc_title': 'Sommaire',
            'intro': 'Introduction',
            'params_intro': 'Introduction aux Paramètres',
            'params_advanced': 'Paramètres Avancés',
            'global_settings': 'Paramètres Globaux',
            'tree_name': 'Nom de l\'Arbre',
            'bg_color': 'Couleur de Fond',
            'basic_shape': 'Paramètres de Forme de Base',
            'trunk_length': 'Longueur du Tronc',
            'min_branch': 'Longueur Minimale de Branche',
            'max_depth': 'Profondeur Maximale',
            'leaf_threshold': 'Seuil de Feuille',
            'max_inclination': 'Inclinaison Maximale de Branche',
            'angle_range': 'Plage d\'Angle de Bifurcation',
            'angle_dist': 'Courbe de Distribution de Probabilité d\'Angle',
            'angle_example': 'Exemple de Référence',
            'decay_range': 'Plage de Décroissance de Longueur',
            'decay_dist': 'Courbe de Distribution de Probabilité de Décroissance',
            'decay_example': 'Exemple de Référence',
            'trunk_leaf': 'Paramètres de Tronc/Feuilles',
            'color': 'Couleur',
            'weight': 'Poids',
            'line_width': 'Rapport de Largeur de Ligne',
            'fallen_leaf': 'Système de Feuilles Mortes',
            'fallen_density': 'Densité de Feuilles Mortes',
            'fallen_dispersion': 'Dispersion des Feuilles Mortes',
            'param_group': 'Système de Groupes de Paramètres',
            'why_param_group': 'Pourquoi des Groupes de Paramètres sont Nécessaires',
            'basic_ops': 'Opérations de Base',
            'typical_height': 'Hauteur Efficace Typique',
            'weight_calc': 'Calcul du Poids',
            'param_example': 'Exemple de Référence',
            'import_export': 'Importer/Exporter des Espèces d\'Arbres',
            'render_window': 'Fenêtre de Rendu',
        },
        'ru': {
            'toc_title': 'Содержание',
            'intro': 'Введение',
            'params_intro': 'Введение в Параметры',
            'params_advanced': 'Продвинутые Параметры',
            'global_settings': 'Глобальные Настройки',
            'tree_name': 'Имя Дерева',
            'bg_color': 'Цвет Фона',
            'basic_shape': 'Параметры Основной Формы',
            'trunk_length': 'Длина Ствола',
            'min_branch': 'Минимальная Длина Ветви',
            'max_depth': 'Максимальная Глубина',
            'leaf_threshold': 'Порог Листа',
            'max_inclination': 'Максимальный Наклон Ветви',
            'angle_range': 'Диапазон Угла Ветвления',
            'angle_dist': 'Кривая Распределения Вероятности Угла',
            'angle_example': 'Пример для Справки',
            'decay_range': 'Диапазон Убывания Длины',
            'decay_dist': 'Кривая Распределения Вероятности Убывания',
            'decay_example': 'Пример для Справки',
            'trunk_leaf': 'Настройки Ствола/Листьев',
            'color': 'Цвет',
            'weight': 'Вес',
            'line_width': 'Коэффициент Толщины Линии',
            'fallen_leaf': 'Система Опавших Листьев',
            'fallen_density': 'Плотность Опавших Листьев',
            'fallen_dispersion': 'Рассеяние Опавших Листьев',
            'param_group': 'Система Групп Параметров',
            'why_param_group': 'Почему Нужны Группы Параметров',
            'basic_ops': 'Базовые Операции',
            'typical_height': 'Типичная Эффективная Высота',
            'weight_calc': 'Расчёт Веса',
            'param_example': 'Пример для Справки',
            'import_export': 'Импорт/Экспорт Видов Деревьев',
            'render_window': 'Окно Рендеринга',
        },
    }

    t = toc_translations.get(lang_code, toc_translations['en'])

    return f'''<nav class="toc">
    <div class="toc-title">{t['toc_title']}</div>
    <ul>
        <li><a href="#intro-{lang_code}">{t['intro']}</a></li>
        <li><a href="#params-intro-{lang_code}">{t['params_intro']}</a></li>
        <li class="toc-collapsible">
            <a href="#params-advanced-{lang_code}"><span class="toc-collapse-toggle" onclick="event.preventDefault(); event.stopPropagation(); toggleCollapse(this)">▼</span>{t['params_advanced']}</a>
            <div class="toc-collapsible-content">
                <ul>
                    <li class="toc-h3"><a href="#global-settings-{lang_code}">{t['global_settings']}</a></li>
                    <li class="toc-h4"><a href="#tree-name-{lang_code}">{t['tree_name']}</a></li>
                    <li class="toc-h4"><a href="#bg-color-{lang_code}">{t['bg_color']}</a></li>
                    <li class="toc-h3"><a href="#basic-shape-{lang_code}">{t['basic_shape']}</a></li>
                    <li class="toc-h4"><a href="#trunk-length-{lang_code}">{t['trunk_length']}</a></li>
                    <li class="toc-h4"><a href="#min-branch-{lang_code}">{t['min_branch']}</a></li>
                    <li class="toc-h4"><a href="#max-depth-{lang_code}">{t['max_depth']}</a></li>
                    <li class="toc-h4"><a href="#leaf-threshold-{lang_code}">{t['leaf_threshold']}</a></li>
                    <li class="toc-h4"><a href="#max-inclination-{lang_code}">{t['max_inclination']}</a></li>
                    <li class="toc-h3"><a href="#angle-range-{lang_code}">{t['angle_range']}</a></li>
                    <li class="toc-h4"><a href="#angle-dist-{lang_code}">{t['angle_dist']}</a></li>
                    <li class="toc-h4"><a href="#angle-example-{lang_code}">{t['angle_example']}</a></li>
                    <li class="toc-h3"><a href="#decay-range-{lang_code}">{t['decay_range']}</a></li>
                    <li class="toc-h4"><a href="#decay-dist-{lang_code}">{t['decay_dist']}</a></li>
                    <li class="toc-h4"><a href="#decay-example-{lang_code}">{t['decay_example']}</a></li>
                    <li class="toc-h3"><a href="#trunk-leaf-{lang_code}">{t['trunk_leaf']}</a></li>
                    <li class="toc-h4"><a href="#color-{lang_code}">{t['color']}</a></li>
                    <li class="toc-h4"><a href="#weight-{lang_code}">{t['weight']}</a></li>
                    <li class="toc-h4"><a href="#line-width-{lang_code}">{t['line_width']}</a></li>
                    <li class="toc-h3"><a href="#fallen-leaf-{lang_code}">{t['fallen_leaf']}</a></li>
                    <li class="toc-h4"><a href="#fallen-density-{lang_code}">{t['fallen_density']}</a></li>
                    <li class="toc-h4"><a href="#fallen-dispersion-{lang_code}">{t['fallen_dispersion']}</a></li>
                    <li class="toc-h3"><a href="#param-group-{lang_code}">{t['param_group']}</a></li>
                    <li class="toc-h4"><a href="#why-param-group-{lang_code}">{t['why_param_group']}</a></li>
                    <li class="toc-h4"><a href="#basic-ops-{lang_code}">{t['basic_ops']}</a></li>
                    <li class="toc-h4"><a href="#typical-height-{lang_code}">{t['typical_height']}</a></li>
                    <li class="toc-h4"><a href="#weight-calc-{lang_code}">{t['weight_calc']}</a></li>
                    <li class="toc-h4"><a href="#param-example-{lang_code}">{t['param_example']}</a></li>
                </ul>
            </div>
        </li>
        <li><a href="#import-export-{lang_code}">{t['import_export']}</a></li>
        <li><a href="#render-window-{lang_code}">{t['render_window']}</a></li>
    </ul>
</nav>'''


def get_bar_chart_html(values: list, labels: list) -> str:
    """生成柱状图HTML"""
    bars_html = '\n'.join([
        f'        <div class="bar-chart-bar" style="height: {v*100:.0f}%" data-value="{v:.2f}"></div>'
        for v in values
    ])
    labels_html = '\n'.join([
        f'        <div class="bar-chart-label">{l}</div>'
        for l in labels
    ])
    return f'''<div class="bar-chart">
    <div class="bar-chart-container">
{bars_html}
    </div>
    <div class="bar-chart-labels">
{labels_html}
    </div>
</div>'''


def process_content_with_charts(content: str, lang_code: str) -> str:
    """处理内容，将柱状图代码块转换为HTML"""
    # 松树角度分布
    pine_angle_values = [1.00, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.20]
    pine_angle_labels = ['0°', '12.86°', '25.71°', '38.57°', '51.43°', '64.29°', '77.14°', '90°']

    # 绿树衰减分布
    green_decay_values = [0.84, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.70]
    green_decay_labels = ['0.60', '0.65', '0.70', '0.75', '0.79', '0.84', '0.89', '0.94']

    # 添加语言后缀到ID
    def add_lang_suffix(match):
        id_val = match.group(1)
        return f'id="{id_val}-{lang_code}"'

    # 处理ID
    content = re.sub(r'id="([^"]+)"', add_lang_suffix, content)

    # 插入柱状图HTML
    # 在角度参考示例后面插入松树柱状图
    angle_chart = get_bar_chart_html(pine_angle_values, pine_angle_labels)
    decay_chart = get_bar_chart_html(green_decay_values, green_decay_labels)

    # 标记柱状图位置并替换
    # 这个在parse_markdown中已经跳过了，需要在正确位置插入

    return content


def generate_html():
    """生成最终HTML文件"""
    # 读取所有语言的内容
    contents = {}
    for lang_code, lang_info in LANGUAGES.items():
        filepath = SCRIPT_DIR / lang_info['file']
        if filepath.exists():
            contents[lang_code] = read_md_file(filepath)
        else:
            print(f"Warning: {filepath} not found, skipping.")

    if not contents:
        print("Error: No content files found!")
        return

    # 构建HTML
    html_parts = ['''<!DOCTYPE html>
<html lang="zh-CN">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>分形树桌面壁纸 帮助</title>
<style>
    body {
        font-family: "Microsoft YaHei", "PingFang SC", sans-serif;
        max-width: 780px;
        margin: 0 auto;
        padding: 32px 24px 48px;
        color: #333;
        line-height: 1.7;
        background: #fcfaf5;
    }
    h1 {
        text-align: center;
        color: #5a3e28;
        border-bottom: 2px solid #d4c4a8;
        padding-bottom: 12px;
        margin-bottom: 28px;
    }
    h2 {
        color: #6b4c30;
        margin-top: 32px;
        border-left: 4px solid #c4a87c;
        padding-left: 12px;
    }
    h3 {
        color: #7a5c3e;
        margin-top: 20px;
    }
    h4 {
        color: #8a6c4e;
        margin-top: 16px;
    }
    table {
        width: 100%;
        border-collapse: collapse;
        margin: 12px 0 20px;
    }
    th, td {
        border: 1px solid #ddd;
        padding: 8px 12px;
        text-align: left;
        font-size: 14px;
    }
    th {
        background: #f0e8d8;
        color: #5a3e28;
    }
    tr:nth-child(even) {
        background: #faf7f0;
    }
    code {
        background: #f0e8d8;
        padding: 2px 6px;
        border-radius: 3px;
        font-size: 13px;
    }
    pre {
        background: #f0e8d8;
        padding: 12px 16px;
        border-radius: 4px;
        overflow-x: auto;
        font-size: 13px;
        line-height: 1.5;
    }
    blockquote {
        background: #fff8e8;
        border-left: 4px solid #e0c878;
        padding: 10px 16px;
        margin: 16px 0;
        font-size: 14px;
        border-radius: 0 4px 4px 0;
    }
    hr {
        border: none;
        border-top: 1px solid #d4c4a8;
        margin: 24px 0;
    }
    .bar-chart {
        background: #f8f4ec;
        border: 1px solid #d4c4a8;
        border-radius: 4px;
        padding: 16px;
        margin: 16px auto;
        display: block;
        width: 356px;
    }
    .bar-chart-container {
        display: flex;
        align-items: flex-end;
        height: 200px;
        width: 324px;
        gap: 4px;
        padding: 0 8px;
    }
    .bar-chart-bar {
        flex: 1;
        background: #7cb87c;
        border-radius: 2px 2px 0 0;
        position: relative;
        min-width: 30px;
        transition: height 0.3s;
    }
    .bar-chart-bar:hover {
        background: #5a9a5a;
    }
    .bar-chart-bar::after {
        content: attr(data-value);
        position: absolute;
        top: -20px;
        left: 50%;
        transform: translateX(-50%);
        font-size: 10px;
        color: #6b4c30;
        opacity: 0;
        transition: opacity 0.2s;
    }
    .bar-chart-bar:hover::after {
        opacity: 1;
    }
    .bar-chart-labels {
        display: flex;
        margin-top: 8px;
        padding: 0 8px;
        width: 324px;
    }
    .bar-chart-label {
        flex: 1;
        text-align: center;
        font-size: 11px;
        color: #6b4c30;
        min-width: 30px;
    }
    .toc {
        position: fixed;
        right: 20px;
        top: 50%;
        transform: translateY(-50%);
        background: rgba(255, 252, 245, 0.95);
        border: 1px solid #d4c4a8;
        border-radius: 8px;
        padding: 16px 20px;
        box-shadow: 0 4px 12px rgba(90, 62, 40, 0.15);
        max-width: 180px;
        font-size: 13px;
        max-height: 70vh;
        overflow-y: auto;
    }
    .toc-title {
        font-weight: bold;
        color: #5a3e28;
        margin-bottom: 12px;
        padding-bottom: 8px;
        border-bottom: 1px solid #e8dcc8;
    }
    .toc ul {
        list-style: none;
        padding: 0;
        margin: 0;
    }
    .toc li {
        margin: 6px 0;
    }
    .toc a {
        color: #6b4c30;
        text-decoration: none;
        display: block;
        padding: 3px 8px;
        border-radius: 4px;
        transition: all 0.2s;
    }
    .toc a:hover {
        background: #f0e8d8;
        color: #5a3e28;
    }
    .toc .toc-h3 {
        padding-left: 12px;
        font-size: 12px;
    }
    .toc .toc-h4 {
        padding-left: 20px;
        font-size: 11px;
        color: #8a6c4e;
    }
    .toc .toc-collapsible {
        position: relative;
    }
    .toc .toc-collapsible > a {
        display: inline-block;
    }
    .toc .toc-collapse-toggle {
        display: inline-block;
        width: 14px;
        height: 14px;
        cursor: pointer;
        text-align: center;
        line-height: 14px;
        color: #8a6c4e;
        font-size: 10px;
        vertical-align: baseline;
        user-select: none;
        margin-right: 4px;
        text-decoration: none;
    }
    .toc .toc-collapse-toggle:hover {
        color: #5a3e28;
    }
    .toc .toc-collapsed .toc-collapse-toggle {
        transform: rotate(-90deg);
    }
    .toc .toc-collapsible-content {
        overflow: hidden;
        transition: max-height 0.3s ease-out;
    }
    .toc .toc-collapsed .toc-collapsible-content {
        max-height: 0 !important;
    }
    h2.highlight, h3.highlight, h4.highlight {
        animation: highlight-flash 0.8s ease-out;
    }
    @keyframes highlight-flash {
        0% { background: rgba(196, 168, 124, 0.4); }
        100% { background: transparent; }
    }
    /* 语言选择器样式 */
    .lang-selector {
        position: fixed;
        top: 20px;
        right: 20px;
        z-index: 1000;
    }
    .lang-selector select {
        padding: 8px 12px;
        border: 1px solid #d4c4a8;
        border-radius: 6px;
        background: rgba(255, 252, 245, 0.95);
        color: #5a3e28;
        font-size: 14px;
        cursor: pointer;
        box-shadow: 0 2px 8px rgba(90, 62, 40, 0.1);
    }
    .lang-selector select:hover {
        border-color: #c4a87c;
    }
    .lang-selector select:focus {
        outline: none;
        border-color: #c4a87c;
        box-shadow: 0 0 0 2px rgba(196, 168, 124, 0.3);
    }
    .content-section {
        display: none;
    }
    .content-section.active {
        display: block;
    }
    @media (max-width: 1200px) {
        body {
            padding: 32px 12px 48px 12px;
            margin-right: 140px;
        }
        .toc {
            right: 8px;
            max-width: 120px;
            padding: 12px 14px;
            font-size: 12px;
        }
        .toc a {
            padding: 3px 6px;
        }
        .lang-selector {
            right: 8px;
            top: 10px;
        }
        .lang-selector select {
            padding: 6px 10px;
            font-size: 13px;
        }
    }
    @media (max-width: 600px) {
        body {
            margin-right: 0;
            padding: 60px 8px 48px 8px;
        }
        .toc {
            position: static;
            transform: none;
            max-width: none;
            width: 100%;
            margin-bottom: 20px;
            box-sizing: border-box;
            max-height: none;
        }
        .toc ul {
            display: flex;
            flex-wrap: wrap;
            gap: 4px;
        }
        .toc li {
            margin: 0;
        }
        .toc a {
            white-space: nowrap;
        }
        .toc .toc-h3, .toc .toc-h4 {
            padding-left: 8px;
        }
        .lang-selector {
            position: fixed;
            right: 8px;
            top: 8px;
        }
    }
</style>
</head>
<body>

<div class="lang-selector">
    <select id="langSelect" onchange="switchLanguage(this.value)">
        <option value="zh">中文</option>
        <option value="en">English</option>
        <option value="ja">日本語</option>
        <option value="ko">한국어</option>
        <option value="es">Español</option>
        <option value="de">Deutsch</option>
        <option value="fr">Français</option>
        <option value="ru">Русский</option>
    </select>
</div>
''']

    # 添加各语言内容
    for lang_code in LANGUAGES.keys():
        if lang_code not in contents:
            continue

        lang_info = LANGUAGES[lang_code]
        toc_html = build_toc(lang_code)

        # 处理内容，添加柱状图
        content = contents[lang_code]
        content = process_content_with_charts(content, lang_code)

        # 在内容中插入柱状图（在特定位置）
        # 松树角度分布柱状图
        angle_chart = get_bar_chart_html([1.00, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.20],
                                          ['0°', '12.86°', '25.71°', '38.57°', '51.43°', '64.29°', '77.14°', '90°'])
        # 绿树衰减分布柱状图
        decay_chart = get_bar_chart_html([0.84, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.70],
                                          ['0.60', '0.65', '0.70', '0.75', '0.79', '0.84', '0.89', '0.94'])

        # 在h4标题后插入柱状图（通过标记位置）
        # 这里我们在解析后的HTML中找到合适的位置插入

        html_parts.append(f'''
<div class="content-section" id="content-{lang_code}" lang="{lang_info['lang']}">
{toc_html}
{content}
</div>
''')

    # 添加JavaScript
    html_parts.append('''
<script>
function toggleCollapse(toggleBtn) {
    const collapsible = toggleBtn.closest('.toc-collapsible');
    const content = collapsible.querySelector('.toc-collapsible-content');

    if (collapsible.classList.contains('toc-collapsed')) {
        collapsible.classList.remove('toc-collapsed');
        content.style.maxHeight = content.scrollHeight + 'px';
    } else {
        collapsible.classList.add('toc-collapsed');
        content.style.maxHeight = content.scrollHeight + 'px';
        requestAnimationFrame(() => {
            content.style.maxHeight = '0';
        });
    }
}

// 初始化折叠区域（默认折叠）
function initCollapsible() {
    document.querySelectorAll('.toc-collapsible').forEach(collapsible => {
        const content = collapsible.querySelector('.toc-collapsible-content');
        collapsible.classList.add('toc-collapsed');
        content.style.maxHeight = '0';
    });
}

// 目录点击跳转（黄金分割比位置）
function initTocLinks() {
    document.querySelectorAll('.toc a').forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            const targetId = this.getAttribute('href').substring(1);
            const targetElement = document.getElementById(targetId);
            if (targetElement) {
                const windowHeight = window.innerHeight;
                const goldenRatio = 0.382;
                const offset = windowHeight * goldenRatio;
                const elementRect = targetElement.getBoundingClientRect();
                const scrollTop = window.pageYOffset + elementRect.top - offset;

                window.scrollTo({
                    top: Math.max(0, scrollTop),
                    behavior: 'smooth'
                });

                targetElement.classList.remove('highlight');
                void targetElement.offsetWidth;
                targetElement.classList.add('highlight');
            }
        });
    });
}

// 切换语言
function switchLanguage(lang) {
    // 隐藏所有内容
    document.querySelectorAll('.content-section').forEach(section => {
        section.classList.remove('active');
    });
    // 显示选中语言的内容
    const targetSection = document.getElementById('content-' + lang);
    if (targetSection) {
        targetSection.classList.add('active');
    }
    // 重新初始化折叠和链接
    initCollapsible();
    initTocLinks();
    // 滚动到顶部
    window.scrollTo({ top: 0, behavior: 'smooth' });
    // 保存语言选择
    localStorage.setItem('help-lang', lang);
}

// 页面加载时初始化
document.addEventListener('DOMContentLoaded', function() {
    // 恢复上次选择的语言
    const savedLang = localStorage.getItem('help-lang') || 'zh';
    document.getElementById('langSelect').value = savedLang;
    switchLanguage(savedLang);
});
</script>

</body>
</html>
''')

    # 写入文件
    output_path = SCRIPT_DIR / 'help.html'
    with open(output_path, 'w', encoding='utf-8') as f:
        f.write('\n'.join(html_parts))

    print(f"Generated: {output_path}")
    print(f"Languages included: {list(contents.keys())}")


if __name__ == '__main__':
    generate_html()