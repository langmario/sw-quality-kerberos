# Testdokumentation

Folgende Tests wurden durchgeführt, um die Funktionalität sicherzustellen. Es handelt sich dabei um Unit-Tests.

Die Tests wurden hierbei in zwei Teile aufgeteilt. Einmal wird getestet, ob eine Eingabe richtig erkannt wird. Im anderen Test wird überprüft, ob die richtige Briefanrede generiert wird.


## Test der Namenserkennung
| Eingabe | Erwarteter Vorname | Erwarteter Nachname | Testergebnis |
| - | - | - | - |
| Sandra Berger | Sandra | Berger | ✅ |
| Maria von Leuthäuser-Schnarrenberger | Maria | von Leuthäuser-Schnarrenberger | ✅ |
| von Leuthäuser-Schnarrenberger, Maria | Maria | von Leuthäuser-Schnarrenberger | ✅ |


## Test der Anredenerkennung

| Eingabe | Erwartete Anrede | Testergebnis |
| - | - | - |
| Herr Fridolin Müller | männlich | ✅ |
| Frau Sandra Berger | männlich | ✅ |
| Sandra Berger | --- | ✅ |


## Test der Titelerkennung

| Eingabe | Erwarteter Titel | Testergebnis |
| - | - | - |
| Herr Dr. Sandro Gutmensch | Dr. | ✅ |
| Herr Dr. rer. nat. Sandro Gutmensch | Dr. (Dr. rer. nat.) | ✅ |
| Herr Prof. Dr. rer. nat. Dr. phil. Sandro Gutmensch | Prof., Dr. (Dr. rer. nat., Dr. phil.) | ✅ |


## Allgemeine Tests

| Eingabe |  Erwarteter Vorname | Erwarteter Nachname | Erwarteter Titel | Erwartete Anrede | Testergebnis |
| - | - | - | - | - | - |
| Herr Prof. Dr. rer. nat. Dr.-Ing. Dr. h.c. mult. Fridolin Müller | Fridolin | Müller | Prof., Dr. | männlich | ✅ |
| Señor Salvador Gonzales | Salvador | Gonzales | --- | männlich | ✅ |
| Herr Tobias Raphael Meier-Müller | Tobias Raphael | Meier-Müller | --- | männlich | ✅ |

<!-- ❌  oder ✅ -->