using System; 

// Programme principal
partial class Programme { // partial permet de séparer en plusieurs fichiers une même classe
    public static void Main(){
        string[] horairesDuJour;
        Liaison liaisonAller = saisirLiaison();
        Traversee traverseeAller = new Traversee(liaisonAller);
        traverseeAller.date = saisirDate();


        // récupération des horaires du jour et de la liaison choisie
        horairesJour(liaisonAller, traverseeAller.date[0], out horairesDuJour); // date[0] correspond au jour
        traverseeAller.heure = saisirHoraire(horairesDuJour);

        afficherTraversee(traverseeAller);


        // saisie des passagers et des véhicules
        List<Passager> passagers = new List<Passager>(); // On ne sait pas combien il y aura de passagers en avance

        do {
            passagers.Add(saisirPassager());
        } while (autrePassager());

        List<Vehicule> vehicules = new List<Vehicule>(); // On ne sait pas combien il y aura de véhicules en avance

        if (vehicule()){
            do {
                vehicules.Add(saisirVehicule());
            } while (autreVehicule());
        }


        Trajet trajetAller = new Trajet(traverseeAller, passagers, vehicules); // plus simple à passer en argument
        trajetAller.prix = calculPrixTrajet(trajetAller);


        // calcul du prix de l'aller 
        double prixAller = calculPrixTrajet(trajetAller);

        
        // saisie du trajet retour s'il y en a un
        bool retour = trajetRetour();

        if (retour){
            // on créée un nouveau trajet comme l'aller sauf que la liaison n'est pas saisie, ni les passagers et véhicules
            Liaison liaisonRetour;

            // si la valeur de la liaison de l'aller est divisible par 2 
            // (donc Lorient-Groix ou Quiberon-Lepalais), on choisis la liaison avec la valeur d'avant (donc Groix-Lorient ou Lepalais-Quiberon)
            if ((int) liaisonAller % 2 == 0){
                liaisonRetour = (Liaison)((int)liaisonAller - 1);
            }
            // et inversement
            else {
                liaisonRetour = (Liaison)((int)liaisonAller + 1);
            }

            Traversee traverseeRetour = new Traversee(liaisonRetour);
            traverseeRetour.date = saisirDate();

            // récupération des horaires du jour et de la liaison choisie
            horairesJour(liaisonRetour, traverseeRetour.date[0], out horairesDuJour); // date[0] correspond au jour
            traverseeRetour.heure = saisirHoraire(horairesDuJour);

            Trajet trajetRetour = new Trajet(traverseeRetour, passagers, vehicules);
            
            double prixRetour = calculPrixTrajet(trajetRetour); // calcul du prix du retour

            afficherPrixTotal(prixAller + prixRetour);
        }
        else {
            afficherPrixTotal(prixAller);
        }
    }

    static double calculPrixTrajet(Trajet trajet){
        // récupère le tarif de chaque véhicule et passager pour avoir le tarif total
        double prixTotal = 0;
        double prixPassager, prixVehicule;

        int indexPassager = 0;
        int indexVehicule = 0;

        bool recupDonnees = true; // true: la récupération des tarifs s'est bien passée, sinon false

        // tant que la récupération des données s'est bien passée et qu'il reste des passagers ou des véhicules à comptabiliser
        while (recupDonnees && (indexPassager < trajet.passagers.Count() || indexVehicule < trajet.vehicules.Count())){
            if (indexPassager < trajet.passagers.Count()){
                // on récupère le prix 
                recupDonnees = tarifPassager(trajet.passagers[indexPassager].categorie, trajet.traversee.liaison, out prixPassager);
                // on l'ajoute au total si tout s'est bien passé
                if (recupDonnees){
                    prixTotal += prixPassager;
                }
                indexPassager++; // passager suivant
            }

            if (indexVehicule < trajet.vehicules.Count()){
                // on récupère le prix 
                recupDonnees = tarifVehicule(trajet.vehicules[indexVehicule].categorie, trajet.traversee.liaison, out prixVehicule);
                // on l'ajoute au total si tout s'est bien passé 
                if (recupDonnees){
                    prixTotal += prixVehicule;
                }
                indexVehicule++; // véhicule suivant
            }
        }
        return prixTotal;
    }
}
