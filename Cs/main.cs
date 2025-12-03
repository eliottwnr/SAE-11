using System; 
using System.Text.Json;

// Programme principal
partial class Programme { // partial permet de séparer en plusieurs fichiers une même classe
    public static void Main(){
        string[] horairesDuJour;

        Liaison liaisonAller = saisirLiaison();
        string nomReservation = saisirNomReservation();

        Reservation traverseeAller = new Reservation(nomReservation, liaisonAller);

        uint[] dateTemp = saisirDate();
        traverseeAller.date = dateTemp[2].ToString("0000") + "-" + dateTemp[1].ToString("00") + "-" + dateTemp[0].ToString("00");


        // récupération des horaires du jour et de la liaison choisie
        horairesJour(liaisonAller, dateTemp[0], out horairesDuJour); // date[0] correspond au jour
        uint[] horaireTemp = saisirHoraire(horairesDuJour);
        traverseeAller.heure = horaireTemp[0].ToString("00") + ":" + horaireTemp[1].ToString("00");

        // debug
        // afficherTraversee(traverseeAller);


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
        List<Trajet> trajets = new List<Trajet>();
        trajets.Add(trajetAller);

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

            Reservation traverseeRetour = new Reservation(nomReservation, liaisonRetour);
            dateTemp = saisirDate();
            traverseeRetour.date = dateTemp[2] + "-" + dateTemp[1] + "-" + dateTemp[0];


            // récupération des horaires du jour et de la liaison choisie
            horairesJour(liaisonRetour, dateTemp[0], out horairesDuJour); // date[0] correspond au jour
            horaireTemp = saisirHoraire(horairesDuJour);
            traverseeRetour.heure = horaireTemp[0] + ":" + horaireTemp[1];


            Trajet trajetRetour = new Trajet(traverseeRetour, passagers, vehicules);
            
            // debug 
            // afficherTraversee(traverseeRetour);

            double prixRetour = calculPrixTrajet(trajetRetour); // calcul du prix du retour

            afficherPrixTotal(prixAller + prixRetour);

            trajets.Add(trajetRetour);
        }
        else {
            afficherPrixTotal(prixAller);
        }

        creerJson(trajets);
    }

    static double calculPrixTrajet(Trajet trajet){
        // récupère le tarif de chaque véhicule et passager pour avoir le tarif total
        double prixTotal = 0;
        double prixPassager, prixVehicule;

        bool recupDonnees = true; // true: la récupération des tarifs s'est bien passée, sinon false

        foreach (Passager p in trajet.passagers){
            recupDonnees = tarifPassager(p.categorie, trajet.reservation.idLiaison, out prixPassager);

            if (recupDonnees){
                prixTotal += prixPassager;
            }
        }

        foreach (Vehicule v in trajet.vehicules){
            recupDonnees = tarifVehicule(v.categorie, trajet.reservation.idLiaison, out prixVehicule);

            if (recupDonnees){
                // puisque la quantitée est donnée pour chaque Vehicule
                for (int i = 0; i < v.quantite; i++){
                    prixTotal += prixVehicule;
                }
            }
        }

        return prixTotal;
    }

    static void creerJson(List<Trajet> trajets){
        string json = JsonSerializer.Serialize(trajets, new JsonSerializerOptions {
            WriteIndented = true, // fais de beaux retours à la ligne dans le .json
            IncludeFields = true, // permet d'inclure tous les champs de trajet
            Converters = { 
                new System.Text.Json.Serialization.JsonStringEnumConverter() 
                }
        });

        File.WriteAllText("../Web/reservation.json", json);    
    }
}
