using System;

partial class Programme {
    struct Traversee {
        public uint liaison;
        public uint[] date;
        public uint[] heure;

        public Traversee(uint l){
            liaison = l;

            date = new uint[3]; // Jour, Mois, Année 
            heure = new uint[2]; // Heures, Minutes
        }
    }
}
