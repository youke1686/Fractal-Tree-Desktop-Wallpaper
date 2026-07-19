# Arbre Fractal Fond d'Écran - Guide d'Utilisation

> **Note**: Ce document a été traduit par IA depuis la version originale en chinois. Si vous trouvez des erreurs ou des expressions peu claires, veuillez vous référer au fichier original `help.md`.

## Introduction

`Arbre Fractal Fond d'Écran` peut générer aléatoirement un bel arbre. Grâce à son puissant système de paramètres, il peut simuler **presque tous les types** d'arbres dans le monde, y compris ceux qui **existent dans la nature** et ceux qui **n'existent que dans votre imagination**.

### Qu'est-ce qu'un `Arbre Fractal`?

Un `Arbre Fractal` est une structure d'arbre **générée récursivement** — chaque branche a plusieurs sous-branches, chaque sous-branch contient plusieurs sous-branches, et ainsi de suite, formant une structure arborescente.  
Dans `Arbre Fractal Fond d'Écran`, pour réduire les calculs de rendu et améliorer la contrôlabilité, nous n'utilisons que des arbres binaires, ce qui signifie que chaque branche a au maximum deux sous-branches.

Pour obtenir une structure d'arbre aussi réaliste que possible, `Arbre Fractal Fond d'Écran` fournit de nombreux paramètres pour contrôler la distance et l'angle entre les nœuds, la couleur des lignes, etc.

---

## Introduction aux Paramètres

Si vous êtes nouveau avec `Arbre Fractal Fond d'Écran`, ne vous laissez pas intimider par le grand nombre de paramètres — vous n'avez besoin d'ajuster que quelques paramètres clés pour créer un arbre unique. Essayez de modifier la `Plage d'Angle de Bifurcation` et la `Plage de Décroissance de Longueur` pour voir comment la forme de l'arbre change ; ajustez ensuite la **couleur** dans les `Paramètres des Feuilles` pour ajouter de la couleur à votre arbre. Une fois familiarisé avec les opérations de base, vous pouvez explorer davantage les **Groupes de Paramètres** et les **Courbes de Distribution** pour faire de chaque arbre une véritable œuvre d'art.

Voici un tutoriel de base sur les paramètres ↓

### 1. Plage d'Angle de Bifurcation

`Plage d'Angle de Bifurcation` contrôle la plage de l'angle de déviation (en degrés) des sous-branches par rapport à leur branche parent. La courbe de distribution contrôle la probabilité des valeurs d'angle dans la plage (bien sûr, les courbes de distribution ne sont pas couvertes dans ce tutoriel de base, gardez donc la distribution uniforme par défaut).

- Quand la `Plage d'Angle de Bifurcation` est **globalement grande**, la cime de l'arbre apparaît **clairsemée et duveteuse**.
- Quand la `Plage d'Angle de Bifurcation` est **globalement petite**, la cime de l'arbre apparaît **dense et compacte**.
- Quand la `Plage d'Angle de Bifurcation` a un **intervalle large**, la cime de l'arbre apparaît **chaotique et désordonnée**.
- Quand la `Plage d'Angle de Bifurcation` a un **intervalle étroit**, la cime de l'arbre apparaît **ordonnée et régulière**.

### 2. Plage de Décroissance de Longueur

`Plage de Décroissance de Longueur` contrôle la plage du rapport de la longueur de la sous-branch par rapport à la branche parent. La courbe de distribution contrôle la probabilité des valeurs de rapport dans la plage (là encore, les courbes de distribution ne sont pas couvertes dans ce tutoriel de base, gardez la distribution uniforme par défaut).

Par exemple, si une bifurcation obtient une valeur aléatoire de **0,7** dans la plage prédéfinie, la longueur de la sous-branch sera de **70%** de la longueur de la branche parent.

- Quand la `Plage de Décroissance de Longueur` est **globalement grande**, la cime de l'arbre apparaît **énorme**.
- Quand la `Plage de Décroissance de Longueur` est **globalement petite**, la cime de l'arbre apparaît **petite et clairsemée**.
- Quand la `Plage de Décroissance de Longueur` a un **intervalle large**, la cime de l'arbre apparaît **variée et étagée**.
- Quand la `Plage de Décroissance de Longueur` a un **intervalle étroit**, la cime de l'arbre apparaît **uniforme et dense**.

> Notez que sans exigences particulières, la valeur maximale de la `Plage de Décroissance de Longueur` ne devrait pas dépasser **0,95**, sinon la cime de l'arbre sera trop dense, ou même croîtra indéfiniment (limitée uniquement par la `Profondeur Maximale`), ce qui affecte non seulement les performances de rendu mais rend aussi la cime extrêmement artificielle.

### 3. Paramètres des Feuilles

`Paramètres des Feuilles` contrôle l'apparence des feuilles. En ajustant la couleur, le poids, la densité et la largeur de ligne des feuilles, vous pouvez obtenir différents effets de feuilles. (Pour le tutoriel de base, modifier uniquement les couleurs suffit.)

Vous pouvez commencer par ajouter plusieurs nuances de vert avec une certaine variation pour voir l'effet.

### 4. Couleur de Fond

Comme son nom l'indique, `Couleur de Fond` contrôle la couleur de fond du canevas.  
Vous pouvez utiliser un ton clair similaire aux feuilles pour un aspect plus naturel.

Pour des suggestions de couleurs plus professionnelles, vous pouvez consulter l'[Outil de Cercle de Couleurs | Adobe Express](https://color.adobe.com/create/color-wheel).

---

## Paramètres Avancés

Cette section fournit des définitions détaillées et l'utilisation de chaque paramètre.

Tous les paramètres en pixels représentent des longueurs lorsque la hauteur de la fenêtre est de 1080 pixels standard. Le rendu réel est mis à l'échelle proportionnellement en fonction de la hauteur de la fenêtre.

---

### 1. Paramètres Globaux

Les paramètres globaux ne sont inclus dans aucun groupe de paramètres et affectent directement le résultat.

#### 1.1 Nom de l'Arbre

`Nom de l'Arbre` est utilisé pour donner un nom à votre arbre.  
Il est affiché dans la liste de l'interface principale et utilisé comme préfixe du nom de fichier par défaut pour les images exportées. Les chaînes vides sont automatiquement corrigées en **"Sans titre"**.

#### 1.2 Couleur de Fond

`Couleur de Fond` est utilisée pour contrôler la couleur de fond du canevas.  
Utilisée pour remplir la couleur de fond de la fenêtre et la couleur de fond de l'image de fond d'écran.

---

### 2. Paramètres de Forme de Base

Les paramètres suivants contrôlent directement la forme de base et les conditions d'arrêt de l'arbre.

#### 2.1 Longueur du Tronc

`Longueur du Tronc` est la longueur initiale (en pixels) de la première branche émanant de la racine de l'arbre. La racine est positionnée à 10% au-dessus du bas du canevas.  
Elle détermine l'échelle de base de tout l'arbre et affecte directement la taille finale de l'arbre.

#### 2.2 Longueur Minimale de Branche

`Longueur Minimale de Branche` est l'une des conditions d'arrêt de l'arbre. Quand la longueur d'une branche tombe en dessous de ce seuil, aucune sous-branch supplémentaire n'est générée.  
Elle contrôle la finesse des extrémités de la cime de l'arbre et la taille de la cime.

#### 2.3 Profondeur Maximale

`Profondeur Maximale` est l'une des conditions d'arrêt de l'arbre. Quand le niveau de récursion d'une branche (la racine est le niveau 0, les sous-branches de la racine sont le niveau 1, etc.) atteint cette valeur, la bifurcation s'arrête.  
Elle empêche la cime de l'arbre de devenir trop dense ou de croître indéfiniment, ce qui affecterait les performances ou ferait même geler l'application.

> Notez que dans les premiers stades de création, il est recommandé de régler la `Profondeur Maximale` sur **10 ~ 30**, puis d'ajuster selon les besoins pour éviter les gels lors des tests dus à des paramètres inappropriés.

#### 2.4 Seuil de Feuille

`Seuil de Feuille` détermine si la branche actuelle doit être rendue comme une brindille ou comme une feuille. Basé sur la longueur de la branche pour déterminer le rôle visuel. Quand la longueur de la branche actuelle est inférieure à ce seuil, la branche est rendue comme une feuille ; au-dessus du seuil, elle est rendue comme un tronc.  
Il contrôle l'effet visuel des feuilles et affecte la forme de la cime de l'arbre.

> Il n'est généralement pas recommandé de l'utiliser pour ajuster directement la densité des feuilles, car cela est lié à la forme des feuilles. D'autres paramètres doivent être ajustés à la place.

#### 2.5 Inclinaison Maximale de Branche

`Inclinaison Maximale de Branche` est l'une des conditions d'arrêt de l'arbre. Elle contrôle la limite supérieure (en degrés) de l'angle absolu des sous-branches (l'angle par rapport à la direction verticale vers le haut). Toute sous-branch dont l'angle dépasse cette valeur ne sera pas créée et se terminera directement.  
Elle empêche les branches de pendre et maintient la forme de l'arbre.

---

### 3. Plage d'Angle de Bifurcation

`Plage d'Angle de Bifurcation` est la plage d'intervalle de l'angle de déviation échantillonné indépendamment pour chaque sous-branch. Deux sous-branches dévient chacune vers un côté de la branche parent. La plage d'échantillonnage est [Minimum, Maximum] (degrés).  
Elle contrôle la forme des branches et affecte l'ordre et le schéma de croissance des branches.

#### 3.1 Courbe de Distribution de Probabilité d'Angle

Ouvrez l'éditeur de graphique à barres via le bouton `Courbe de Distribution` : cliquez sur les barres pour ajuster les hauteurs des barres, avec une plage de hauteur de `0,02 ~ 1,0`.

**Méthode d'échantillonnage pour chaque sous-branch**: Divisez l'intervalle d'angle en 8 sous-intervalles égaux, sélectionnez d'abord un sous-intervalle en utilisant les hauteurs des barres de la courbe de distribution comme poids, puis prenez une valeur aléatoire uniforme dans ce sous-intervalle. Si aucune courbe de distribution n'est spécifiée, une valeur aléatoire uniforme est prise dans l'intervalle global.

En résumé, plus la barre est haute, plus la probabilité que les valeurs proches soient sélectionnées comme angles de branches est élevée.

#### 3.2 Exemple de Référence

L'arbre par défaut `Pin` a une plage d'angle de bifurcation de `[0, 90]`, avec une courbe de distribution de probabilité d'angle approximativement comme suit :

<!-- BAR_CHART: [1.00, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.20] | [0°, 12.86°, 25.71°, 38.57°, 51.43°, 64.29°, 77.14°, 90°] -->

On voit que les angles de bifurcation du pin sont plus susceptibles d'être autour de 0 et 90 degrés. C'est-à-dire que les branches du pin sont plus susceptibles d'être presque parallèles ou perpendiculaires à la branche parent, plutôt que de croître en diagonale.  
L'effet réel est que les pins croissent surtout droit, mais font parfois de grands changements de direction, fournissant une forme similaire au pin tordu et vigoureux réel.  
Être plus susceptible d'être parallèle à la branche parent permet de maintenir la forme de base de la cime sans être trop chaotique.

---

### 4. Plage de Décroissance de Longueur

`Plage de Décroissance de Longueur` est la plage d'intervalle du rapport de décroissance de longueur échantillonné indépendamment pour chaque sous-branch. La longueur réelle de la sous-branch = longueur de la branche parent × rapport de décroissance échantillonné. La plage d'échantillonnage est [Minimum, Maximum] (rapport de décroissance).

#### 4.1 Courbe de Distribution de Probabilité de Décroissance

Similaire à [3.1 Courbe de Distribution de Probabilité d'Angle](#31-courbe-de-distribution-de-probabilité-dangle).

Ouvrez l'éditeur de graphique à barres via le bouton `Courbe de Distribution` : cliquez sur les barres pour ajuster les hauteurs des barres, avec une plage de hauteur de `0,02 ~ 1,0`.

**Méthode d'échantillonnage pour chaque sous-branch**: Divisez l'intervalle d'angle en 8 sous-intervalles égaux, sélectionnez d'abord un sous-intervalle en utilisant les hauteurs des barres de la courbe de distribution comme poids, puis prenez une valeur aléatoire uniforme dans ce sous-intervalle. Si aucune courbe de distribution n'est spécifiée, une valeur aléatoire uniforme est prise dans l'intervalle global.

En résumé, plus la barre est haute, plus la probabilité que les valeurs proches soient sélectionnées comme rapports de décroissance est élevée.

#### 4.2 Exemple de Référence

L'arbre par défaut `Arbre Vert` a une plage de décroissance de longueur de `[0,6, 0,94]`, avec une courbe de distribution de probabilité de décroissance approximativement comme suit :

<!-- BAR_CHART: [0.84, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.70] | [0.60, 0.65, 0.70, 0.75, 0.79, 0.84, 0.89, 0.94] -->

On voit que les rapports de décroissance de l'arbre vert sont plus susceptibles d'être autour de 0,60 et 0,94. C'est-à-dire que les branches de l'arbre vert sont soit presque de la même longueur que la branche parent, soit nettement plus courtes.  
L'effet réel est que l'arbre vert a parfois des branches étendues et parfois des feuilles rassemblées, fournissant une forme étagée et variée.  
Il est à noter que si l'arbre vert choisit une branche courte trop tôt, cela réduira directement la hauteur de tout l'arbre, entraînant une plus grande variation de la hauteur de l'arbre.

---

### 5. Paramètres de Tronc/Feuilles

`Paramètres de Tronc/Feuilles` est une collection de paramètres qui déterminent l'effet visuel des troncs et des feuilles, sans affecter la structure de l'arbre.

Les troncs et les feuilles ont chacun des listes de couleurs indépendantes. Chaque entrée de couleur contient trois attributs :

#### 5.1 Couleur

`Couleur` est un paramètre qui détermine la couleur utilisée pour rendre les troncs ou les feuilles. Supporte la définition de plusieurs couleurs pour les troncs/feuilles, sélectionnées aléatoirement.

#### 5.2 Poids

`Poids` est un paramètre qui détermine la probabilité que cette couleur soit sélectionnée lors du dessin d'un nouveau tronc ou feuille. Plus la valeur est grande, plus la proportion de cette couleur dans les troncs/feuilles est élevée.

#### 5.3 Rapport de Largeur de Ligne

`Rapport de Largeur de Ligne` est un paramètre qui détermine le coefficient de largeur des troncs ou des feuilles. Plus la valeur est grande, plus le tronc/la feuille est épais. L'épaisseur réelle dépend aussi de la longueur de la branche.

**Comportement avec Plusieurs Groupes de Paramètres**: Les paramètres de tronc/feuilles ne participent pas à la moyenne pondérée numérique de plusieurs groupes de paramètres. Quand plusieurs groupes de paramètres existent, la liste de couleurs du groupe avec le plus grand poids est utilisée directement.

---

### 6. Système de Feuilles Mortes

`Système de Feuilles Mortes` est une fonction pour ajouter des feuilles mortes au sol. Après qu'une feuille soit dessinée, il y a une probabilité de dessiner une feuille morte supplémentaire de même forme dans la zone du sol.  
Il améliore l'ambiance de l'arbre.

#### 6.1 Densité de Feuilles Mortes

`Densité de Feuilles Mortes` est un paramètre qui détermine la probabilité que les feuilles soient converties en feuilles mortes au sol. La plage de valeurs est `0,0 ~ 1,0`. Plus la valeur est grande, plus il y a de feuilles mortes.  
Le nombre réel de feuilles mortes dépend aussi de la densité de la cime de l'arbre (le nombre de feuilles sur l'arbre).

#### 6.2 Dispersion des Feuilles Mortes

`Dispersion des Feuilles Mortes` est un paramètre qui détermine la plage de distribution des feuilles mortes, spécifiquement la plage de décalage horizontal (en pixels) par rapport à la feuille originale et la plage de décalage vertical (en pixels) par rapport à la position du sol. Plus la valeur est grande, plus la plage de distribution des feuilles mortes est large.

Le décalage vertical par rapport à la position du sol utilise une distribution triangulaire (moyenne de deux valeurs aléatoires uniformes).  
Le décalage horizontal par rapport à la feuille originale utilise une distribution uniforme, car l'aléatoire d'un grand nombre de feuilles atteint déjà un effet similaire à une distribution normale.

---

### 7. Système de Groupes de Paramètres

`Groupe de Paramètres` est le concept d'abstraction central d'`Arbre Fractal Fond d'Écran`. Une configuration d'espèce d'arbre unique peut contenir un ou plusieurs groupes de paramètres, chaque groupe ayant un ensemble indépendant de paramètres.

#### 7.0 Pourquoi des Groupes de Paramètres sont Nécessaires

Imaginez que vous voulez concevoir un arbre avec un tronc droit mais une cime étendue.  
Si vous n'utilisez qu'un seul ensemble de paramètres, le tronc et la cime ne peuvent avoir la même forme, rendant les effets complexes impossibles.

En fait, c'était le problème réel que j'ai rencontré en concevant l'`Érable`.  
Pour cela, j'ai eu deux idées :

La première idée était d'incorporer la coordonnée y de la branche dans les "paramètres de paramètres", ce qui pouvait implémenter directement des paramètres qui changent avec la hauteur. En d'autres termes, les paramètres pouvaient être écrits comme des fonctions de y.  
Cela correspond bien à la pensée d'un programmeur et peut réaliser un contrôle de forme plus complexe et précis. Cependant, c'est clairement peu intuitif. En pratique, cela nécessite une pensée abstraite assez complexe et l'utilisation de calculatrices ou même d'outils comme Desmos pour étudier les graphiques de fonctions et ajuster les paramètres de manière répétée.

J'ai donc choisi la deuxième idée : le système de groupes de paramètres.  
En définissant directement plusieurs ensembles de paramètres et en appliquant différents groupes de paramètres à différentes hauteurs, les formes du tronc et de la cime peuvent être distinguées.

Pendant le développement réel, pour des transitions fluides, j'ai choisi d'appliquer un mélange pondéré inverse des paramètres de différents groupes de paramètres.

#### 7.1 Opérations de Base

Dans `Contrôle de Plusieurs Groupes de Paramètres`:

- **Liste Déroulante**: Sélectionnez un groupe de paramètres à éditer. Cliquez pour modifier le nom du groupe de paramètres.
- **「+」**: Ajoutez un nouveau groupe de paramètres (copie les paramètres du groupe de paramètres actuellement sélectionné).
- **「✕」**: Supprimez le groupe de paramètres actuellement sélectionné.

#### 7.2 Hauteur Efficace Typique

`Hauteur Efficace Typique` est la hauteur d'ancrage (en pixels) de ce groupe de paramètres, utilisée pour les calculs de mélange pondéré de plusieurs groupes de paramètres.  
Elle contrôle la position approximative où le groupe de paramètres prend effet. Plus la branche est proche de la hauteur efficace typique du sol, plus le poids du groupe de paramètres est élevé.

En résumé, les paramètres prendront effet près de la `Hauteur Efficace Typique`.

#### 7.3 Calcul du Poids

Quand une branche est à la hauteur $h$, le poids d'origine du groupe $i$ est :

$$w_i = \min\left(\frac{1}{|h - t_i|},\ 128\right)$$

Où $t_i$ est la hauteur typique du groupe $i$. Le poids est fixé à 128 quand la distance est 0 pour éviter la division par zéro. Après normalisation de tous les poids :

- **Paramètres scalaires ordinaires** (plage d'angle, plage de décroissance, branche minimale, profondeur maximale, seuil de feuille, paramètres de feuilles mortes, longueur du tronc, inclinaison de branche): Moyenne pondérée.
- **`Paramètres de Tronc/Feuilles`**: Utilisation directe de la liste de couleurs du groupe au poids le plus élevé.
- **`Courbes de Distribution`** (distribution d'angle, distribution de décroissance): Moyenne pondérée par barre.

Quand il n'y a qu'un seul groupe de paramètres, le calcul de mélange est ignoré et tous les paramètres de ce groupe sont utilisés directement.

#### 7.4 Exemple de Référence

L'arbre par défaut `Érable` a deux groupes de paramètres : "Tronc" et "Cime". La seule différence entre eux est la `Plage d'Angle de Bifurcation`, la `Plage de Décroissance de Longueur` et la `Hauteur Efficace Typique`.

**Groupe de paramètres "Tronc"**:
- La hauteur efficace typique est `0`, ce qui signifie que plus près du sol, le poids du groupe de paramètres est élevé.
- La plage d'angle de bifurcation est `0 ~ 10` distribution uniforme, maintenant la forme droite du tronc.
- La plage de décroissance de longueur est `0,8 ~ 0,95` distribution uniforme, assurant que le tronc est long et réduisant les branches inférieures.

**Groupe de paramètres "Cime"**:
- La hauteur efficace typique est `180`, ce qui signifie fondamentalement que plus loin du sol, le poids du groupe de paramètres est élevé.
- La plage d'angle de bifurcation est `0 ~ 60` avec une probabilité élevée aux deux extrémités, maintenant la forme duveteuse de la cime.
- La plage de décroissance de longueur est `0,3 ~ 0,95` avec une probabilité élevée à l'extrémité grande et une probabilité basse mais possible à l'extrémité petite, maintenant la forme étagée de la cime et augmentant la diversité de la cime.

---

## Importer/Exporter des Espèces d'Arbres

Le programme enregistre toutes les espèces d'arbres personnalisées dans des fichiers `<Nom de l'Arbre>.json` dans le dossier `AppData\Local\Arbre Fractal Fond d'Écran\trees`. Vous pouvez cliquer sur le bouton `Ouvrir le Dossier d'Arbres` en bas à gauche de la fenêtre de paramètres pour l'ouvrir rapidement.

- Pour importer une espèce d'arbre, placez simplement le fichier `<Nom de l'Arbre>.json` à importer dans ce dossier, puis redémarrez le programme.
- Pour exporter une espèce d'arbre, copiez simplement le fichier `<Nom de l'Arbre>.json` à exporter.

---

## Fenêtre de Rendu

Cliquez sur n'importe quel bouton d'espèce d'arbre dans l'interface principale ou sur Aperçu dans l'interface d'édition pour ouvrir la fenêtre de rendu de l'arbre fractal. Le glisser-déposer et le zoom sont pris en charge.

- **Actualiser**: Réinitialise la graine aléatoire et redessine. Chaque clic produit une forme différente.
- **Enregistrer comme Image**: Rend en PNG à la taille de la fenêtre, enregistre sur le bureau sous `<Nom de l'Arbre>_<Horodatage>.png`. La fenêtre reste ouverte après achèvement.
- **Définir comme Fond d'Écran**: Rend en BMP à 2x la résolution d'écran, appelle l'API système pour définir comme fond d'écran Windows. La fenêtre se ferme automatiquement après achèvement.