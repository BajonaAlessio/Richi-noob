# Struttura

## classi:

- Pilota
- Moto
- Squadra
- Player
- Gara
- Circuito
- Race
- DTO su ogni Classe 

## Relazioni:

- Pilota con caratteristiche specifiche sceglie una squadra 
- Nella squadra scelta ha la posibilita di scegliere una moto da 3 modelli storici del club
- La moto scelta ha delle caratteristiche specifiche che influiscono direttamente nella corsa
- Cosi viene creato il Player(giocatore)
- il giocatore puo scegliere il paese dove fare la gara, questa classe ha delle caratteristiche climatiche.
- Dipendendo il paese scelto il giocatore ha la posibilità di scelgiere uno dei 3 circuiti proprosti
- Il circuito ha delle caratteristiche proprie (lunghezza, giri, ecc).
- Si avvia il gioco per vedere che giocatore vince


## Extra dentro le classi:

- Tempo del giocatore 
- clima in gara: Pioggia/sole
- caratteristiche moto
- Lunghezza circuito
- N. di Giri
- N. Curve
- Posizione di fine corsa


# Step by Step

- Creo Program.cs:
costruisco i builder e i service
- voglio incapsuplare tutti i file dentro le proprie cartelle, mi creo una struttura globale e creo le cartelle
- creo i models, sono le classi basi, dentro la cartella (Models)
- creo i DTO, cio che il serve fa vedere(sempre Models).
- Creo i service delle mie classi basi, dentro la cartella Service
- Creo la cartella utils e dentro creo i metodi che incapsulano i compiti che condiviono le mie classi:
 - Id Generator (generare un id per ogni singolo file Json)
 - JsonHelper (Gestisco serilize e deserialize dei file json)
 - LoggerHelper (creo uno storico delle azioni dentro la mia app, e scrivo un txt)
 - RaceCalculator (Calcolo il tempo dei player dentro una stessa gara e le ordino per dare una posizione)
- Si serializzano i file json dentro i folders




RaceCalculator:

1) Tempo base in funzione della lunghezza e del numero di giri
2) Fattore curve, normalizzato
3) Penalizzazione per potenza nei circuiti con molte curve
4) Bonus per potenza nei circuiti con poche curve
5) Bonus per inclinazione (miglioramento in curva)
6) Bonus per grip (stabilità generale)
7) Bonus per accelerazione (uscita migliore dalle curve)
8) Combinare tutto
9) Variazione casuale per renderlo meno deterministico (+/- 3%)
10) Mai meno di 1 secondo


RaceController:

1) Calcolare il tempo di ogni pilota
2) Cercare il circuito (prendiamo il primo disponibile)
3) Calcolare il tempo usando RaceCalculator
4) Ordinare manualmente per TempoFinale
5) Assegnare le posizioni dopo l'ordinamento