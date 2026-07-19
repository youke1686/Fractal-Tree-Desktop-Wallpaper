# Fraktalbaum Desktop-Hintergrundbild - Benutzerhandbuch

> **Hinweis**: Dieses Dokument wurde von der Originalversion auf Chinesisch durch KI übersetzt. Wenn Sie Fehler oder unklare Ausdrücke finden, beachten Sie bitte die Originaldatei `help.md`.

## Einführung

`Fraktalbaum Desktop-Hintergrundbild` kann zufällig einen schönen Baum generieren. Dank seines leistungsstarken Parametersystems kann es fast **jede Art** von Baum in der Welt simulieren, einschließlich derer, die **in der Natur existieren**, und derer, die **nur in Ihrer Vorstellung existieren**.

### Was ist ein `Fraktalbaum`?

Ein `Fraktalbaum` ist eine **rekursiv generierte** Baumstruktur — jeder Ast hat mehrere Unteräste, jeder Unterast enthält mehrere Unteräste, und so weiter, was zu einer baumartigen Struktur führt.  
In `Fraktalbaum Desktop-Hintergrundbild` verwenden wir nur Binärbäume, um die Rendering-Berechnung zu reduzieren und die Kontrollierbarkeit zu verbessern, was bedeutet, dass jeder Ast maximal zwei Unteräste hat.

Um eine möglichst realistische Baumstruktur zu erreichen, bietet `Fraktalbaum Desktop-Hintergrundbild` zahlreiche Parameter zur Kontrolle des Abstands und Winkels zwischen Knoten, der Linienfarben usw.

---

## Parameter-Einführung

Wenn Sie neu bei `Fraktalbaum Desktop-Hintergrundbild` sind, lassen Sie sich nicht von der großen Anzahl an Parametern einschüchtern — Sie müssen nur einige Kernparameter anpassen, um einen einzigartigen Baum zu erstellen. Versuchen Sie, den `Verzweigungswinkelbereich` und den `Längenabnahmebereich` zu ändern, um zu sehen, wie sich die Baumform verändert; passen Sie dann die **Farbe** in den `Blatteinstellungen` an, um Ihrem Baum Farbe zu verleihen. Wenn Sie mit den grundlegenden Operationen vertraut sind, können Sie weiter die **Parametergruppen** und **Verteilungskurven** erkunden, um jeden Baum zu einem wahren Kunstwerk zu machen.

Im Folgenden finden Sie ein grundlegendes Parameter-Tutorial ↓

### 1. Verzweigungswinkelbereich

`Verzweigungswinkelbereich` steuert den Bereich des Ablenkungswinkels (in Grad) der Unteräste relativ zum Elternast. Die Verteilungskurve steuert die Wahrscheinlichkeit der Winkelwerte im Bereich (natürlich sind Verteilungskurven nicht Teil dieses grundlegenden Tutorials, behalten Sie also die Standard-Gleichverteilung bei).

- Wenn der `Verzweigungswinkelbereich` **generell groß** ist, erscheint die Baumkrone **dünn und flauschig**.
- Wenn der `Verzweigungswinkelbereich` **generell klein** ist, erscheint die Baumkrone **dicht und kompakt**.
- Wenn der `Verzweigungswinkelbereich` einen **breiten Intervall** hat, erscheint die Baumkrone **chaotisch und unordentlich**.
- Wenn der `Verzweigungswinkelbereich` einen **engen Intervall** hat, erscheint die Baumkrone **ordentlich und gleichmäßig**.

### 2. Längenabnahmebereich

`Längenabnahmebereich` steuert den Bereich des Verhältnisses der Unterastlänge zur Elternastlänge. Die Verteilungskurve steuert die Wahrscheinlichkeit der Verhältniswerte im Bereich (auch dies ist nicht Teil dieses grundlegenden Tutorials, behalten Sie also die Standard-Gleichverteilung bei).

Wenn zum Beispiel eine Verzweigung einen zufälligen Wert von **0,7** aus dem voreingestellten Bereich erhält, wird die Unterastlänge **70%** der Elternastlänge betragen.

- Wenn der `Längenabnahmebereich` **generell groß** ist, erscheint die Baumkrone **riesig**.
- Wenn der `Längenabnahmebereich` **generell klein** ist, erscheint die Baumkrone **klein und dünn**.
- Wenn der `Längenabnahmebereich` einen **breiten Intervall** hat, erscheint die Baumkrone **abgestuft und vielfältig**.
- Wenn der `Längenabnahmebereich` einen **engen Intervall** hat, erscheint die Baumkrone **gleichmäßig und dicht**.

> Beachten Sie, dass der Maximalwert des `Längenabnahmebereichs` ohne besondere Anforderungen **0,95** nicht überschreiten sollte, sonst wird die Baumkrone zu dicht oder wächst sogar unendlich (nur durch die `Maximale Tiefe` begrenzt), was nicht nur die Rendering-Leistung beeinträchtigt, sondern auch die Baumkrone extrem unnatürlich aussehen lässt.

### 3. Blatteinstellungen

`Blatteinstellungen` steuert das Erscheinungsbild der Blätter. Durch Anpassung von Blattfarbe, Gewicht, Dichte und Linienbreite können Sie verschiedene Blatteffekte erzielen. (Für das grundlegende Tutorial genügt es, die Farben zu ändern.)

Sie können damit beginnen, mehrere Grüntöne mit gewisser Variation hinzuzufügen, um den Effekt zu sehen.

### 4. Hintergrundfarbe

Wie der Name schon sagt, steuert `Hintergrundfarbe` die Hintergrundfarbe der Leinwand.  
Sie können einen hellen, blattähnlichen Farbton für ein natürlicheres Aussehen verwenden.

Für professionellere Farbvorschläge können Sie das [Farbrad-Werkzeug | Adobe Express](https://color.adobe.com/create/color-wheel) konsultieren.

---

## Fortgeschrittene Parameter

Dieser Abschnitt bietet detaillierte Definitionen und Verwendung jedes Parameters.

Alle Parameter in Pixeln repräsentieren Längen bei einer Standardfensterhöhe von 1080 Pixeln. Das tatsächliche Rendering skaliert proportional basierend auf der Fensterhöhe.

---

### 1. Globale Einstellungen

Die globalen Einstellungen sind in keiner Parametergruppe enthalten und beeinflussen das Ergebnis direkt.

#### 1.1 Baumname

`Baumname` wird verwendet, um Ihrem Baum einen Namen zu geben.  
Er wird in der Liste der Hauptschnittstelle angezeigt und als Standard-Dateinamenpräfix für exportierte Bilder verwendet. Leere Zeichenfolgen werden automatisch zu **"Unbenannt"** korrigiert.

#### 1.2 Hintergrundfarbe

`Hintergrundfarbe` wird verwendet, um die Hintergrundfarbe der Leinwand zu steuern.  
Sie wird zum Füllen der Fensterhintergrundfarbe und der Hintergrundfarbe des Hintergrundbildes verwendet.

---

### 2. Grundlegende Formparameter

Die folgenden Parameter steuern direkt die Grundform und die Abbruchbedingungen des Baumes.

#### 2.1 Stammlänge

`Stammlänge` ist die Anfangslänge (in Pixeln) des ersten Astes, der von der Wurzel ausgeht. Die Wurzel befindet sich 10% über dem unteren Rand der Leinwand.  
Sie bestimmt die Basisskala des gesamten Baumes und beeinflusst direkt die endgültige Größe des Baumes.

#### 2.2 Minimale Astlänge

`Minimale Astlänge` ist eine der Abbruchbedingungen des Baumes. Wenn die Astlänge unter diesen Schwellenwert fällt, werden keine weiteren Unteräste generiert.  
Sie steuert die Feinheit der Enden der Baumkrone und die Größe der Krone.

#### 2.3 Maximale Tiefe

`Maximale Tiefe` ist eine der Abbruchbedingungen des Baumes. Wenn die Rekursionsebene eines Astes (die Wurzel ist Ebene 0, Unteräste von der Wurzel sind Ebene 1 usw.) diesen Wert erreicht, wird die Verzweigung gestoppt.  
Sie verhindert, dass die Baumkrone zu dicht wird oder unendlich wächst, was die Leistung beeinträchtigt oder die Anwendung sogar einfrieren lässt.

> Beachten Sie, dass in den frühen Phasen der Erstellung die `Maximale Tiefe` auf **10 ~ 30** eingestellt werden sollte, und später je nach Bedarf angepasst werden sollte, um Einfrierungen während des Testens aufgrund unangemessener Parameter zu vermeiden.

#### 2.4 Blattschwellenwert

`Blattschwellenwert` bestimmt, ob der aktuelle Ast als Zweig oder als Blatt gerendert werden soll. Basierend auf der Astlänge wird die visuelle Rolle bestimmt. Wenn die aktuelle Astlänge unter diesem Schwellenwert liegt, wird der Ast als Blatt gerendert; über dem Schwellenwert wird er als Stamm gerendert.  
Er steuert den visuellen Effekt der Blätter und beeinflusst die Form der Baumkrone.

> Generell wird nicht empfohlen, dies direkt zur Anpassung der Blattdichte zu verwenden, da dies mit der Blattform zusammenhängt. Andere Parameter sollten stattdessen angepasst werden.

#### 2.5 Maximale Astneigung

`Maximale Astneigung` ist eine der Abbruchbedingungen des Baumes. Sie steuert die Obergrenze (in Grad) des absoluten Winkels der Unteräste (der Winkel relativ zur vertikal aufwärts gerichteten Richtung). Jeder Unterast, dessen Winkel diesen Wert überschreitet, wird nicht erstellt und endet direkt.  
Sie verhindert, dass Äste herabhängen, und erhält die Form des Baumes.

---

### 3. Verzweigungswinkelbereich

`Verzweigungswinkelbereich` ist der Intervallbereich des Ablenkungswinkels, der für jeden Unterast unabhängig abgetastet wird. Zwei Unteräste weichen jeweils zu beiden Seiten des Elternastes ab. Der Abtastbereich ist [Minimum, Maximum] (Grad).  
Er steuert die Form der Äste und beeinflusst die Gleichmäßigkeit und das Wachstumsmuster der Äste.

#### 3.1 Winkelwahrscheinlichkeits-`Verteilungskurve`

Öffnen Sie den Balkendiagramm-Editor über die Schaltfläche `Verteilungskurve`: Klicken Sie auf die Balken, um die Balkenhöhen anzupassen. Der Höhenbereich ist `0,02 ~ 1,0`.

**Abtastmethode für jeden Unterast**: Teilen Sie den Winkelintervall in 8 gleiche Unterintervalle, wählen Sie zuerst ein Unterintervall mit den Balkenhöhen der Verteilungskurve als Gewichte, und nehmen Sie dann einen gleichmäßig zufälligen Wert innerhalb dieses Unterintervalls. Wenn keine Verteilungskurve angegeben ist, wird ein gleichmäßig zufälliger Wert aus dem globalen Intervall genommen.

Einfach ausgedrückt: Je höher der Balken, desto höher ist die Wahrscheinlichkeit, dass nahegelegene Werte als Astwinkel ausgewählt werden.

#### 3.2 Referenzbeispiel

Der Standardbaum `Kiefer` hat einen Verzweigungswinkelbereich von `[0, 90]`, mit einer Winkelwahrscheinlichkeits-Verteilungskurve ungefähr wie folgt:

<!-- BAR_CHART: [1.00, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.20] | [0°, 12.86°, 25.71°, 38.57°, 51.43°, 64.29°, 77.14°, 90°] -->

Man sieht, dass die Verzweigungswinkel der Kiefer mit höherer Wahrscheinlichkeit um 0 und 90 Grad liegen. Das heißt, die Äste der Kiefer sind eher fast parallel oder senkrecht zum Elternast, statt diagonal zu wachsen.  
Der tatsächliche Effekt ist, dass Kiefern meist gerade wachsen, aber manchmal große Richtungsänderungen machen, was eine Form ähnlich der verdrehten und kräftigen Form echter Kiefern bietet.  
Dabei ist eine größere Wahrscheinlichkeit, parallel zum Elternast zu sein, um die Grundform der Baumkrone nicht zu chaotisch zu halten.

---

### 4. Längenabnahmebereich

`Längenabnahmebereich` ist der Intervallbereich des Längenabnahmeverhältnisses, der für jeden Unterast unabhängig abgetastet wird. Die tatsächliche Länge des Unterastes = Länge des Elternastes × abgetastetes Abnahmeverhältnis. Der Abtastbereich ist [Minimum, Maximum] (Abnahmeverhältnis).

#### 4.1 Abnahmewahrscheinlichkeits-`Verteilungskurve`

Ähnlich wie [3.1 Winkelwahrscheinlichkeits-Verteilungskurve](#31-winkelwahrscheinlichkeits-verteilungskurve).

Öffnen Sie den Balkendiagramm-Editor über die Schaltfläche `Verteilungskurve`: Klicken Sie auf die Balken, um die Balkenhöhen anzupassen. Der Höhenbereich ist `0,02 ~ 1,0`.

**Abtastmethode für jeden Unterast**: Teilen Sie den Winkelintervall in 8 gleiche Unterintervalle, wählen Sie zuerst ein Unterintervall mit den Balkenhöhen der Verteilungskurve als Gewichte, und nehmen Sie dann einen gleichmäßig zufälligen Wert innerhalb dieses Unterintervalls. Wenn keine Verteilungskurve angegeben ist, wird ein gleichmäßig zufälliger Wert aus dem globalen Intervall genommen.

Einfach ausgedrückt: Je höher der Balken, desto höher ist die Wahrscheinlichkeit, dass nahegelegene Werte als Abnahmeverhältnis ausgewählt werden.

#### 4.2 Referenzbeispiel

Der Standardbaum `Grüner Baum` hat einen Längenabnahmebereich von `[0,6, 0,94]`, mit einer Abnahmewahrscheinlichkeits-Verteilungskurve ungefähr wie folgt:

<!-- BAR_CHART: [0.84, 0.02, 0.02, 0.02, 0.02, 0.02, 0.02, 0.70] | [0.60, 0.65, 0.70, 0.75, 0.79, 0.84, 0.89, 0.94] -->

Man sieht, dass die Abnahmeverhältnisse des grünen Baums mit höherer Wahrscheinlichkeit um 0,60 und 0,94 liegen. Das heißt, die Äste des grünen Baums sind entweder fast gleich lang wie der Elternast oder deutlich kürzer.  
Der tatsächliche Effekt ist, dass der grüne Baum manchmal ausgebreitete Äste hat und manchmal eingesammelte Blätter, was eine geschichtete und vielfältige Form bietet.  
Es ist erwähnenswert, dass wenn der grüne Baum zu früh einen kurzen Ast wählt, dies direkt die Höhe des gesamten Baumes reduziert, was zu einer größeren Variation der Baumhöhe führt.

---

### 5. Stamm/Blatt-Einstellungen

`Stamm/Blatt-Einstellungen` ist eine Sammlung von Parametern, die den visuellen Effekt von Stämmen und Blättern bestimmen, ohne die Baumstruktur zu beeinflussen.

Stämme und Blätter haben jeweils unabhängige Farblisten. Jeder Farbeingang enthält drei Attribute:

#### 5.1 Farbe

`Farbe` ist ein Parameter, der die zum Rendern von Stämmen oder Blättern verwendete Farbe bestimmt. Es werden mehrere Farben für Stämme/Blätter unterstützt, die zufällig ausgewählt werden.

#### 5.2 Gewicht

`Gewicht` ist ein Parameter, der die Wahrscheinlichkeit bestimmt, dass diese Farbe beim Zeichnen eines neuen Stammes oder Blattes ausgewählt wird. Je größer der Wert, desto höher der Anteil dieser Farbe in Stämmen/Blättern.

#### 5.3 Linienbreitenverhältnis

`Linienbreitenverhältnis` ist ein Parameter, der den Breitenkoeffizienten von Stämmen oder Blättern bestimmt. Je größer der Wert, desto dicker wird der Stamm/das Blatt. Die tatsächliche Dicke hängt auch von der Astlänge ab.

**Verhalten bei mehreren Parametergruppen**: Die Stamm/Blatt-Einstellungen nehmen nicht am numerischen gewichteten Durchschnitt mehrerer Parametergruppen teil. Wenn mehrere Parametergruppen existieren, wird die Farbliste der Parametergruppe mit dem größten Gewicht direkt verwendet.

---

### 6. Fallblatt-System

`Fallblatt-System` ist eine Funktion, um Fallblätter am Boden hinzuzufügen. Nachdem ein Blatt gezeichnet wurde, gibt es eine Wahrscheinlichkeit, ein zusätzliches Fallblatt mit derselben Form im Bodenbereich zu zeichnen.  
Es verbessert die Atmosphäre des Baumes.

#### 6.1 Fallblattdichte

`Fallblattdichte` ist ein Parameter, der die Wahrscheinlichkeit bestimmt, dass Blätter zu Boden-Fallblättern konvertiert werden. Der Wertbereich ist `0,0 ~ 1,0`. Je größer der Wert, desto mehr Fallblätter.  
Die tatsächliche Anzahl der Fallblätter hängt auch von der Dichte der Baumkrone (der Anzahl der Blätter am Baum) ab.

#### 6.2 Fallblattstreuung

`Fallblattstreuung` ist ein Parameter, der den Verteilungsbereich der Fallblätter bestimmt, speziell den horizontalen Versatzbereich (in Pixeln) relativ zum ursprünglichen Blatt und den vertikalen Versatzbereich (in Pixeln) relativ zur Bodenposition. Je größer der Wert, desto größer der Verteilungsbereich der Fallblätter.

Der vertikale Versatz relativ zur Bodenposition verwendet eine Dreiecksverteilung (Durchschnitt von zwei gleichmäßig zufälligen Werten).  
Der horizontale Versatz relativ zum ursprünglichen Blatt verwendet eine Gleichverteilung, da die Zufälligkeit einer großen Anzahl von Blättern bereits einen ähnlichen Effekt wie eine Normalverteilung erzielt.

---

### 7. Parametergruppen-System

`Parametergruppe` ist das zentrale Abstraktionskonzept von `Fraktalbaum Desktop-Hintergrundbild`. Eine einzelne Baumart-Konfiguration kann eine oder mehrere Parametergruppen enthalten, jede Gruppe mit einem unabhängigen Parametersatz.

#### 7.0 Warum Parametergruppen benötigt werden

Stellen Sie sich vor, Sie möchten einen Baum mit einem geraden Stamm, aber einer ausgebreiteten Krone entwerfen.  
Wenn Sie nur einen Parametersatz verwenden, können Stamm und Krone nur dieselbe Form haben, was komplexe Effekte unmöglich macht.

Tatsächlich war dies das reale Problem, das ich beim Entwurf von `Ahorn` angetroffen habe.  
Dafür hatte ich zwei Ideen:

Die erste Idee war, die y-Koordinate des Astes in "Parameter von Parametern" einzubeziehen, was direkt Parameter ermöglichen könnte, die sich mit der Höhe ändern. Mit anderen Worten, Parameter könnten als Funktionen von y geschrieben werden.  
Dies entspricht gut dem Denken eines Programmierers und kann eine komplexere und präzisere Formkontrolle erreichen. Es ist jedoch offensichtlich wenig intuitiv. In der Praxis erfordert es ziemlich komplexes abstraktes Denken und den Einsatz von Taschenrechnern oder sogar Werkzeugen wie Desmos, um Funktionsgraphen zu untersuchen und Parameter wiederholt anzupassen.

Daher wählte ich die zweite Idee: das Parametergruppen-System.  
Durch direkte Definition mehrerer Parametersätze und Anwendung verschiedener Parametergruppen auf verschiedenen Höhen können die Formen von Stamm und Krone unterschieden werden.

Bei der tatsächlichen Entwicklung entschied ich mich für eine inverse gewichtete Mischung von Parametern aus verschiedenen Parametergruppen für sanfte Übergänge.

#### 7.1 Grundlegende Operationen

In `Mehrere Parametergruppen-Steuerung`:

- **Dropdown**: Wählen Sie eine Parametergruppe zur Bearbeitung. Klicken Sie, um den Parametergruppennamen zu ändern.
- **「+」**: Fügen Sie eine neue Parametergruppe hinzu (kopiert die Parameter der aktuell ausgewählten Parametergruppe).
- **「✕」**: Löschen Sie die aktuell ausgewählte Parametergruppe.

#### 7.2 Typische effektive Höhe

`Typische effektive Höhe` ist die Ankerhöhe (in Pixeln) dieser Parametergruppe, die für die gewichtete Mischungsberechnung mehrerer Parametergruppen verwendet wird.  
Sie steuert die ungefähre Position, an der die Parametergruppe wirksam wird. Je näher der Ast an der typischen effektiven Höhe vom Boden ist, desto höher ist das Gewicht der Parametergruppe.

Einfach ausgedrückt werden Parameter in der Nähe der `Typischen effektiven Höhe` wirksam.

#### 7.3 Gewichtungsberechnung

Wenn sich ein Ast auf der Höhe $h$ befindet, ist das Ursprungsgewicht der Gruppe $i$:

$$w_i = \min\left(\frac{1}{|h - t_i|},\ 128\right)$$

Wobei $t_i$ die typische Höhe der Gruppe $i$ ist. Das Gewicht wird auf 128 festgelegt, wenn der Abstand 0 ist, um eine Division durch Null zu vermeiden. Nach der Normalisierung aller Gewichte:

- **Gewöhnliche Skalarparameter** (Winkelbereich, Abnahmebereich, minimaler Ast, maximale Tiefe, Blattschwellenwert, Fallblattparameter, Stammlänge, Astneigung): Gewichteter Durchschnitt.
- **`Stamm/Blatt-Einstellungen`**: Direkte Verwendung der Farbliste der Gruppe mit dem größten Gewicht.
- **`Verteilungskurven`** (Winkelverteilung, Abnahmeverteilung): Gewichteter Durchschnitt pro Balken.

Wenn es nur eine Parametergruppe gibt, wird die Mischungsberechnung übersprungen und alle Parameter dieser Gruppe direkt verwendet.

#### 7.4 Referenzbeispiel

Der Standardbaum `Ahorn` hat zwei Parametergruppen: "Stamm" und "Krone". Der einzige Unterschied zwischen beiden ist der `Verzweigungswinkelbereich`, der `Längenabnahmebereich` und die `Typische effektive Höhe`.

**Parametergruppe "Stamm"**:
- Die typische effektive Höhe ist `0`, was bedeutet, dass je näher am Boden, desto höher das Gewicht der Parametergruppe.
- Der Verzweigungswinkelbereich ist `0 ~ 10` Gleichverteilung, was die aufrechte Form des Stammes beibehält.
- Der Längenabnahmebereich ist `0,8 ~ 0,95` Gleichverteilung, was sicherstellt, dass der Stamm lang ist und die unteren Äste reduziert werden.

**Parametergruppe "Krone"**:
- Die typische effektive Höhe ist `180`, was grundsätzlich bedeutet, dass je weiter vom Boden entfernt, desto höher das Gewicht der Parametergruppe.
- Der Verzweigungswinkelbereich ist `0 ~ 60` mit hoher Wahrscheinlichkeit an beiden Enden, was die flauschige Form der Krone beibehält.
- Der Längenabnahmebereich ist `0,3 ~ 0,95` mit hoher Wahrscheinlichkeit am großen Ende und niedriger, aber möglicher Wahrscheinlichkeit am kleinen Ende, was die geschichtete Form der Krone beibehält und die Vielfalt der Krone erhöht.

---

## Baumarten importieren/exportieren

Das Programm speichert alle benutzerdefinierten Baumarten in `<Baumname>.json`-Dateien im Ordner `AppData\Local\Fraktalbaum Desktop-Hintergrundbild\trees`. Sie können auf die Schaltfläche `Baumordner öffnen` unten links im Parametereinstellungsfenster klicken, um ihn schnell zu öffnen.

- Um eine Baumart zu importieren, platzieren Sie einfach die zu importierende `<Baumname>.json`-Datei in diesem Ordner und starten Sie das Programm neu.
- Um eine Baumart zu exportieren, kopieren Sie einfach die zu exportierende `<Baumname>.json`-Datei.

---

## Rendering-Fenster

Klicken Sie auf eine beliebige Baumart-Schaltfläche in der Hauptschnittstelle oder auf Vorschau in der Bearbeitungsschnittstelle, um das Fraktalbaum-Rendering-Fenster zu öffnen. Ziehen und Zoomen werden unterstützt.

- **Aktualisieren**: Setzt den zufälligen Startwert zurück und zeichnet neu. Jeder Klick erzeugt eine andere Form.
- **Als Bild speichern**: Rendert als PNG in Fenstergröße, speichert auf dem Desktop als `<Baumname>_<Zeitstempel>.png`. Das Fenster bleibt nach Abschluss geöffnet.
- **Als Desktop-Hintergrundbild festlegen**: Rendert als BMP bei 2x Bildschirmauflösung, ruft die System-API auf, um als Windows-Desktop-Hintergrundbild festzulegen. Das Fenster schließt automatisch nach Abschluss.