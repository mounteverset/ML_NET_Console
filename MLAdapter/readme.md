# Programmierprojekt Teil Lukas Evers

## Installationshinweise

- Zu öffnen ist die **MLAdapter.sln** in diesem Verzeichnis mit Visual Studio Professional 2019

- **Verwendete NuGet Pakete in diesem Projekt:**
  - Microsoft.ML v1.5.4
  - Microsoft.ML.CpuMath v1.5.4
  - Microsoft.ML.DataView v1.5.4
  - Microsoft.ML.FastTree v1.5.4  
  
- Neue Trainings- und Testdaten sind im Verzeichnis _grp05/programm/_ zu speichern
- Für Debug und Release ist _"AnyCPU"_ durch _"x64"_ zu ersetzen

![x64](https://i.ibb.co/gthbjg6/x64.jpg)

## Bedienungshinweise

- Die auszuführende Datei ist Program.cs aus dem Projekt _"DummyGUI_MLAdapter"_
- In Program.cs werden die Dateipfade für die einzulesenden CSV Dateien festgelgt, im Moment verweist der Pfad auf den MNIST Datensatz
- Es sind in der Program.cs Datei die Input und Label Spalten festzulegen, im Moment stimmen diese für das Trainieren des MNIST Datensatzes
- Die geladene DataTable wird beim Start einmal komplett in die Konsole geschrieben. Sollte ein umfangreicher Datensatz gewählt worden sein ist es empfehlenswert auf diesen Teil zu verzichten, oder die Funktion _PrintDataTableToConsole_ mit dem cutOff - Parameter zu benutzen um die Anzahl der ausgegeben Reihen zu begrenzen. Das Trainieren des Machine Learning Modells dauert in dem Fall ohnehin sicher schon sehr lange.


    