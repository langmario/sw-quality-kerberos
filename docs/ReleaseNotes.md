# Release Notes

## Startseite
Auf der Startseite kann eine Anrede, welche in das Eingabefeld eingegeben wurde, in folgende Bestandteile aufgeteilt werden.
* Anrede
* Sprache
* Geschlecht
* Titel
* Vorname
* Nachname

Hierbei geschieht dies nur, falls diese aus der Anrede herausgelesen werden können.

Zusätzlich gibt es, als Ausgabe eine ... 
* Standardisierte Briefanrede 
* Option ein Komma an der Briefanrede anzufügen

Das Aufteilen und die Generierung der Briefanrede wird durch das Klicken des Buttons `Senden` ausgelöst. Der\*die Nutzer\*in haben dann die Möglichkeit den Titel mit in der generierten Briefanrede auszugeben.

![Startseite des Kerberos Clients](./image/KerberosClient1.png)

## Hinzufügen einer Sprache
Der\*die Nutzer\*in kann jeder Zeit eine Sprache hinzufügen, die erkannt werden soll. Hierfür kann auf `/language` zugegriffen werden. Der\*die Nutzer\*in bekommt auf dieser Seite eine Übersicht über die abgespeicherte Sprachen.

![Sprachübersicht im Kerberos Client](./image/KerberosClient2_1.png)

Über den Button `Neue Sprache hinzufügen` kann eine neue Sprache hinzugefügt werden. Hierbei muss ein `Sprachschlüssel` (zur eindeutigen Identifizierung) angegeben werden und der Sprache ein `Name` vergeben werden.
![Sprache im Kerberos Client hinzufügen](./image/KerberosClient2_2.png)

Um eine Sprache zu löschen, kann diese in der Übersicht ausgewählt werden und mit Klicken des `Ausgewählte Sprache löschen` Buttons entfernt werden.


## Hinzufügen eines Titels
Der\*die Nutzer\*in kann jeder Zeit einen Titel hinzufügen, der erkannt werden soll. Hierfür kann auf `/titel` zugegriffen werden. Der\*die Nutzer\*in bekommt auf dieser Seite eine Übersicht über die abgespeicherten Titel.

![Titelübersicht im Kerberos Client](./image/KerberosClient3_1.png)

Über den Button `Neuen Titel hinzufügen` kann ein neuer Titel hinzugefügt werden. Hierbei kann ein `Titel` vergeben werden, dem eine beliebige Anzahl an `Aliases` (z.B. eine Abkürzung) hinzugefügt werden können.

![Titel im Kerberos Client hinzufügen](./image/KerberosClient3_2.png)

Um einen Titel zu löschen, kann dieser in der Übersicht ausgewählt werden und mit Klicken des `Ausgewählten Titel löschen` Buttons entfernt werden.



## Hinzufügen einer Anrede
Der\*die Nutzer\*in kann jeder Zeit eine Anrede hinzufügen, die erkannt werden soll. Hierfür kann auf `/salutation` zugegriffen werden. Der\*die Nutzer\*in bekommt auf dieser Seite eine Übersicht über die abgespeicherte Anreden.

![Anredenübersicht im Kerberos Client](./image/KerberosClient4_1.png)

Über den Button `Neue Anrede hinzufügen` kann eine neue Anrede hinzugefügt werden. Hierbei muss im Feld `Anrede` festgelegt werden, wie die Anrede lautet, die erkannt werden soll. Im Feld `Formale Anrede` wird festgelegt, welche Briefanrede aus der `Anrede` generiert werden soll. Im Feld `Sprache` kann festgelegt werden welche Sprache die Anrede hat. Mit dem Feld `Geschlecht` kann das Geschlecht der Anrede ausgewählt werden.

![Anrede im Kerberos Client hinzufügen](./image/KerberosClient4_2.png)

Um eine Anrede zu löschen, kann diese in der Übersicht ausgewählt werden und mit Klicken des `Ausgewählte Anrede löschen` Buttons entfernt werden.