using System; 

// Programme principal
partial class Programme { // partial permet de séparer en plusieurs fichiers une même classe
    public static void Main(){
        string[] horairesDuJour;

        Liaison liaison = saisirLiaison();

        Traversee traversee = new Traversee(liaison);
        
        traversee.date = saisirDate();

        // récupération des horaires du jour et de la liaison choisie
        horairesJour(liaison, traversee.date[0], out horairesDuJour); // date[0] correspond au jour
        traversee.heure = saisirHoraire(horairesDuJour);

        afficherTraversee(traversee);


        List<Passager> passagers = new List<Passager>(); // On ne sait pas combien il y aura de passagers en avance

        do {
            passagers.Add(saisirPassager());
        } while (autrePassager());

        List<Vehicule> vehicules = new List<Vehicule>(); // On ne sait pas combien il y aura de véhicules en avance

        do {
            vehicules.Add(saisirVehicule());
        } while (autreVehicule());

        // test 
        foreach (Vehicule v in vehicules){
            Console.WriteLine(v.quantite.ToString() + ' ' + v.categorie);
        }
    }
}
