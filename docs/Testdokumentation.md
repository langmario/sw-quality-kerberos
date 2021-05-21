# Testdokumentation

Folgende Tests wurden durchgeführt, um die Funktionalität sicherzustellen. Es handelt sich dabei um Unit-Tests. Es wurden hierbei Tests für die Namenserkennung, für die Anredenerkennung, Titelerkennung und allgemeine Tests gemacht.

Um zusätzlich die UI und den User-Fluss zu testen, wurden die Kundentests manuell durchgeführt.

## Test der Namenserkennung (Unit Tests)
| Eingabe | Erwarteter Vorname | Erwarteter Nachname | Testergebnis |
| - | - | - | - |
| Sandra Berger | Sandra | Berger | ✅ |
| Maria von Leuthäuser-Schnarrenberger | Maria | von Leuthäuser-Schnarrenberger | ✅ |
| von Leuthäuser-Schnarrenberger, Maria | Maria | von Leuthäuser-Schnarrenberger | ✅ |


## Test der Anredenerkennung (Unit Tests)

| Eingabe | Erwartetes Geschlecht | Testergebnis |
| - | - | - |
| Herr Fridolin Müller | Männlich | ✅ |
| Frau Sandra Berger | Männlich | ✅ |
| Sandra Berger | --- | ✅ |


## Test der Titelerkennung (Unit Tests)

| Eingabe | Erwarteter Titel | Testergebnis |
| - | - | - |
| Herr Dr. Sandro Gutmensch | Dr. | ✅ |
| Herr Dr. rer. nat. Sandro Gutmensch | Dr. (Dr. rer. nat.) | ✅ |
| Herr Prof. Dr. rer. nat. Dr. phil. Sandro Gutmensch | Prof., Dr. (Dr. rer. nat., Dr. phil.) | ✅ |


## Allgemeine Tests (Unit Tests)

| Eingabe |  Erwarteter Vorname | Erwarteter Nachname | Erwarteter Titel | Erwartetes Geschlecht | Testergebnis |
| - | - | - | - | - | - |
| Herr Prof. Dr. rer. nat. Dr.-Ing. Dr. h.c. mult. Fridolin Müller | Fridolin | Müller | Prof., Dr. | Männlich | ✅ |
| Señor Salvador Gonzales | Salvador | Gonzales | --- | Männlich | ✅ |
| Herr Tobias Raphael Meier-Müller | Tobias Raphael | Meier-Müller | --- | Männlich | ✅ |


## Kundentests (manuell)

| Eingabe | Erwarteter Vorname | Erwarteter Nachname | Erwarteter Titel | Erwartetes Geschlecht | Erwartete Nationalität | Testergebnis |
| - | - | - | - | - | - | - |
Frau Sandra Berger | Sandra | Berger | --- | Weiblich | Deutsch | ✅ |
Herr Dr. Sandro Gutmensch | Sandro | Gutmensch | Dr. | Männlich | Deutsch | ✅ |
Professor Heinrich vom Wald | Heinrich | vom Wald | Prf. | --- | --- | ✅ |
Mrs Doreen Faber | Doreen | Faber | --- | Weiblich | Englisch | ✅ |
Mme. Charlotte Noir | Charlotte | Noir | --- | Weiblich | Französisch | ✅ |
Senor Estobar Gonzales | Estobar | Gonzales | --- | Männlich | Spanisch | ✅ |
Frau Prof. Dr. rer. nat. Maria von Leuthäuser-Schnarrenberger | Maria | von Leuthäuser-Schnarrenberger | Prof, Dr. (rer. nat.) | Weiblich | Deutsch | ✅ |
Herr Dipl. Ing. Max von Müller | Max | von Müller | Dipl. Ing. | Männlich | Deutsch | ✅ |
Dr. Russwurm, Winfried | Winfried | Russwurm | Dr. | --- | --- | ✅ |
Herr Dr. -Ing. Dr. rer. nat. Dr. h.c. mult. Paul Steffens | Paul | Steffens | Dr. (-Ing., rer. nat., h.c. mult.) | Männlich | Deutsch | ✅ |

<!-- ❌  oder ✅ -->