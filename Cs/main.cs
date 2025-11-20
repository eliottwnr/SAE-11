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

        // récupère le tarif de chaque véhicule et passager pour avoir le tarif total
        double prixTotal = 0;
        double prixPassager, prixVehicule;

        int indexPassager = 0;
        int indexVehicule = 0;

        bool recupDonnees = true; // true: la récupération des tarifs s'est bien passée, sinon false

        // tant que la récupération des données s'est bien passée et qu'il reste des passagers ou des véhicules à comptabiliser
        while (recupDonnees && (indexPassager < passagers.Count() || indexVehicule < vehicules.Count())){
            if (indexPassager < passagers.Count()){
                // on récupère le prix 
                recupDonnees = tarifPassager(passagers[indexPassager].categorie, liaison, out prixPassager);
                // on l'ajoute au total si tout s'est bien passé
                if (recupDonnees){
                    prixTotal += prixPassager;
                }
                indexPassager++; // passager suivant
            }

            if (indexVehicule < vehicules.Count()){
                // on récupère le prix 
                recupDonnees = tarifVehicule(vehicules[indexVehicule].categorie, liaison, out prixVehicule);
                // on l'ajoute au total si tout s'est bien passé 
                if (recupDonnees){
                    prixTotal += prixVehicule;
                }
                indexVehicule++; // véhicule suivant
            }
        }

        afficherPrixTotal(prixTotal);
    }
}
