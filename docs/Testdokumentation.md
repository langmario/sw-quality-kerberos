# Testdokumentation

Folgende Tests wurde durchgeführt, um die Funktionalität sicherzustellen. Es handelt sich dabei um Unit-Tests.

Die Tests wurden hierbei in zwei Teile aufgeteilt. Einmal wird getestet, ob eine Eingabe richtig erkannt wird. Der andere testet, ob die richtige Briefanrede generiert wird.


## Test der Namenserkennung
| Eingabe | Erwarteter Vorname | Erwarteter Nachname | Testergebnis |
| - | - | - | - |
| Sandra Berger | Sandra | Berger | ✅ |
| Maria von Leuthäuser-Schnarrenberger | Maria | von Leuthäuser-Schnarrenberger | ✅ |
| von Leuthäuser-Schnarrenberger, Maria | Maria | von Leuthäuser-Schnarrenberger | ✅ |


## Test der Anredenerkennung

| Eingabe | Erwartetes Geschlecht | Testergebnis |
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

| Eingabe |  Erwarteter Vorname | Erwarteter Nachname | Erwarteter Titel | Erwartetes Geschlecht | Testergebnis |
| - | - | - | - | - | - |
| Herr Prof. Dr. rer. nat. Dr.-Ing. Dr. h.c. mult. Fridolin Müller | Fridolin | Müller | Prof., Dr. | männlich | ✅ |
| Señor Salvador Gonzales | Salvador | Gonzales | --- | männlich | ✅ |
| Herr Tobias Raphael Meier-Müller | Tobias Raphael | Meier-Müller | --- | männlich | ✅ |


## Kundentests

| Eingabe | Erwarteter Vorname | Erwarteter Nachname | Erwarteter Titel | Erwartetes Geschlecht | Erwartete Nationalität | Testergebnis |
| - | - | - | - | - | - | - |
Frau Sandra Berger | Sandra | Berger | --- | weiblich | Deutsch | ❌ |
Herr Dr. Sandro Gutmensch | Sandro | Gutmensch | Dr. | männlich | Deutsch | ❌ |
Professor Heinreich Freiherr vom Wald | Heinreich | Freiherr vom Wald | Prf. | männlich | Deutsch | ❌ |
Mrs. Doreen Faber | Doreen | Faber | --- | weiblich | Deutsch | ❌ |
Mme. Charlotte Noir | Charlotte | Noir | --- | weiblich | Englisch | ❌ |
Estobar y Gonzales | Sandra | Berger | --- | weiblich | Deutsch | ❌ |
Frau Prof. Dr. rer. nat. Maria von Leuthäuser-Schnarrenberger | Maria | von Leuthäuser-Schnarrenberger | Dr. (rer. nat.) | weiblich | Deutsch | ❌ |
Herr Dipl. Ing. Max von Müller | Max | von Müller | Dipl. Ing. | männlich | Deutsch | ❌ |
Dr. Russwurm, Winfried | Winfried | Russwurm | Dr. | männlich | Deutsch | ❌ |
Herr Dr.-Ing. Dr. rer. nat. Dr. h.c. mult. Paul Steffens | Paul | Steffens | Dr. (Ing., rer. nat., h.c. mult.) | männlich | Deutsch | ❌ |

<!-- ❌  oder ✅ -->