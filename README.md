# Programmierprojekt: ML.Students

* Teammitglieder:
	1. Orock Soh Talla Anderson Lewis
	2. Lukas Evers	
* Team: 5
* Semester: Wintersemester 2020/2021

# Installationshinweise
<details>
<summary>Aufklappen</summary>

- Die zu startende Datei ist die Datei [MLAdapter](/programm/MLAdapter/MLAdapter.sln) im Verzeichnis programm/MLAdapter
- Es ist Visual Studio Professional 2019 zu verwenden

- Die Versionierung der NuGet-Pakete ist beim Herunterladen auf jeden Fall zu beachten:
	- Microsoft.ML v1.5.4
  	- Microsoft.ML.CpuMath v1.5.4
  	- Microsoft.ML.DataView v1.5.4
  	- Microsoft.ML.FastTree v1.5.4
	- System.Buffers v4.5.1
	- System.CodeDom v.5.0.0
	- System.Runtime.CompilerServices.Unsafe v4.5.3 (**Nicht 5.0.0, welches die neuste Version ist**)
	- System.Threading.Channels v5.0.0
	- System.Threading.Tasks.Extensions v4.5.4
	- System.Numerics.Vector v4.5.0
	- System.Memory v4.5.4
	- System.Reflection.Emit.Lightweight v.4.7.0


- Für Debug und Release ist _"AnyCPU"_ durch _"x64"_ zu ersetzen

![x64](https://i.ibb.co/gthbjg6/x64.jpg)
</details>

# Verwendung der Software

### Start des Programms 
<details>
<summary>Aufklappen</summary>

- Die zu startende Datei ist die Datei [MLAdapter.sln](/programm/MLAdapter/MLAdapter.sln) im Verzeichnis programm/MLAdapter
- Die Projektmappe ist so konfiguriert, dass beim Starten des Projektes in Visual Studio das Projekt "GUI_MLAdapter" von selbst gestartet wird
- Es öffnet sich daraufhin eine Konsolenanwendung

</details>

### Bedienung des Programms

##### Einsatzgebiet des Programm

<details><summary>Aufklappen</summary>

- Das Programm ist für die Mehrklassenklassifizierung (Multiclass Classification) erstellt worden
- Inputs für das Programm beschränken sich auf Zahlenwerte
	- d.h. Strings und kategorielle Werte dürfen nicht als Eingabespalten für das Modell ausgewählt werden
- Das Programm nutzt eine Methode um das Machine Learning Modell zu trainieren
	- d.h. nicht jedes Klassifizierungsproblem ist mit diesem generellen Trainer gut lösbar
- Vorbereitung der Daten:
	- Trainings- und Testdaten sind vor dem Starten des Programm zu trennen
	- Es ist nicht notwendig Spaltennamen/eine Headerzeile zu benutzen
	- Die Werte der Datenreihen sind mit Kommas zu separieren

</details>

##### Funktionen des Programms

<details><summary>CSV Tabelle einlesen und Machine Learning Modell trainieren</summary>


- Trainings- und Testdaten für die Verwendung im Programm sind im Verzeichnis _grp05/programm/DataSets/_ abzulegen

- Es sind alle Dateien in dem Ordner mit der Endung _".csv"_ auswählbar, _".txt"_ oder _".xlsx"_ sind nicht im Programmablauf verfügbar zum Benutzen

- Nach dem Laden wird ein Auszug der Tabelle dargestellt, dies **dient zur Orientierung**, damit die Auswahl der Input- und Labelspalten einfacher ist

- Den **Index der Inputspalten** soll man mit einzelnen Kommas getrennt in die Konsole eingeben

- Für die Labelspalte muss man beachten, dass die Labelspalte nicht bereits als Inputspalte deklariert sein darf

- Nach dem Bestätigen wird die aktuelle Auswahl noch einmal angezeigt

- Falls die Auswahl übernommen wird, beginnt das Programm mit dem Trainieren eines Modells

- Nach dem Abschluss des Trainings wird nach dem Testdatensatz für das Modell gefragt

- Die Genauigkeit des Modells wird basierend darauf berechnet in wieviel % der Fällen das Modell die **tatsächliche Klasse** richtig vorhergesagt hat

- Damit ist das Training abgeschlossen und man gelangt zurück ins Hauptmenü, um die anderen Funktionen des Programmes zu nutzen

</details>

<details><summary>Gespeichertes Machine Learning Modell laden</summary>

- Von einem vorherigen Training gespeicherte Modelle liegen im Ordner _grp05/programm/ML_Models/_ als .zip-Dateien

- Nach dem Laden des Modells gelangt man wieder zurück zum Hauptmenü

- Zu beachten ist: 
	- Möchte man das Modell für Vorhersagen benutzen, **MUSS** die Anzahl und Reihenfolge der Inputspalten dieselbe sein wie die Inputspalten mit denen man das Modell beim Erstellen trainiert und getestet hat

</details>

<details><summary>Machine Learning Modell anwenden</summary>

- Um ein Modell zu benutzen und damit **bisher ungelabelte Daten** zu klassifizieren werden diese Daten wieder als CSV Datei in das Programm gelesen

- Die Datei ist im Ordner _grp05/programm/DataSets/_ abzulegen

- Bei der Auswahl der Inputspalten ist es **WICHTIG**, dass die Reihenfolge und Anzahl den Inputspalten den Daten entspricht, die zum Trainieren des Modells benutzt wurden
	- Es können Fehler auftreten, wenn man ein Modell mit einer Tabelle in Format XY zuerst erstellt, das Modell speichert, ein anderes Modell lädt 
welches aber mit Daten im Format YZ trainiert wurde und dann probiert das Modell anzuwenden, da das Programm noch die Inputspalten von Format XY benutzt.


- Nachdem man die Inputspalten festgelegt hat, erhält man die Prädiktion des ML Modells zurück und kann die Ergebnisse der Klassifizierung in der Spalte ML_Result ansehen. Die Option diese Tabelle abzuspeichern, steht dann zur Verfügung.

</details>

<details><summary>Machine Learning Modell speichern</summary>

- Wenn man ein Modell erstellt hat, erhält man die Option dieses Modell abzuspeichern, um es zu einem späteren Zeitpunkt zu laden und sich den Aufwand des Trainierens zu sparen
- Modelle werden im Ordner _grp05/programm/ML_Models/_ als .zip-Dateien gespeichert

</details>


## Links und Verweise

1. Markdown Syntax: https://docs.gitlab.com/ee/user/markdown.html
2. Git fuer Windows: https://git-scm.com/download/win