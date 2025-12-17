using System; 
using System.Text.Json;

// Programme principal
partial class Programme { // partial permet de séparer en plusieurs fichiers une même classe
    public static void Main(){
        string[] horairesDuJour;

        Liaison liaisonAller = saisirLiaison();
        string nomReservation = saisirNomReservation();

        Reservation traverseeAller = new Reservation(nomReservation, liaisonAller);

        uint[] dateTempAller = saisirDate();
        traverseeAller.date = dateTempAller[2].ToString("0000") + "-" + dateTempAller[1].ToString("00") + "-" + dateTempAller[0].ToString("00");


        // récupération des horaires du jour et de la liaison choisie
        horairesJour(liaisonAller, dateTempAller[0], out horairesDuJour); // date[0] correspond au jour
        uint[] horaireTempAller = saisirHoraire(horairesDuJour);
        traverseeAller.heure = horaireTempAller[0].ToString("00") + ":" + horaireTempAller[1].ToString("00");

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
                // On vérifie qu'il n'y a pas déjà un véhicule de ce type dans la liste
                Vehicule nouveau = saisirVehicule();
                bool estDansListe = false;
                int i = 0;

                while (!estDansListe && i < vehicules.Count()){
                    if (vehicules[i].codeCategorie == nouveau.codeCategorie){
                        estDansListe = true;
                    }
                    i++;
                }

                if (estDansListe){
                    AfficherVehiculeDansListe();
                }
                else {
                    vehicules.Add(nouveau);
                }

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

            uint[] dateTempRetour;
            uint[] horaireTempRetour;
            bool dateValide, horaireValide;

            do {
                dateTempRetour = saisirDate();

                // récupération des horaires du jour et de la liaison choisie
                horairesJour(liaisonRetour, dateTempRetour[0], out horairesDuJour); // date[0] correspond au jour
                horaireTempRetour = saisirHoraire(horairesDuJour);

                dateValide = dateAnterieure(dateTempAller, dateTempRetour);
                horaireValide = horaireAnterieur(horaireTempAller, horaireTempRetour);

                if (!dateValide){
                    afficherDateNonAnterieure();
                    attendre();
                }

                // si c'est le même jour mais que l'horaire retour est avant l'aller
                else if (dateTempRetour[0] == dateTempAller[0] && !horaireValide){
                        afficherHoraireNonAnterieure();
                        attendre();
                }
            } while (!dateValide || (dateTempRetour[0] == dateTempAller[0] && !horaireValide));

            traverseeRetour.date = dateTempRetour[2] + "-" + dateTempRetour[1] + "-" + dateTempRetour[0];
            traverseeRetour.heure = horaireTempRetour[0] + ":" + horaireTempRetour[1];


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
            recupDonnees = tarifPassager(p.codeCategorie, trajet.reservation.idLiaison, out prixPassager);

            if (recupDonnees){
                prixTotal += prixPassager;
            }
        }

        foreach (Vehicule v in trajet.vehicules){
            recupDonnees = tarifVehicule(v.codeCategorie, trajet.reservation.idLiaison, out prixVehicule);

            if (recupDonnees){
                // puisque la quantitée est donnée pour chaque Vehicule
                for (int i = 0; i < v.quantite; i++){
                    prixTotal += prixVehicule;
                }
            }
        }

        return prixTotal;
    }

    static bool dateAnterieure(uint[] date1, uint[] date2){
        // Renvoie true si date1 <= date2, sinon false
        bool estAnterieure = false;
        bool memeAnnee = (date1[2] == date2[2]);
        bool memeMois = (date1[1] == date2[1]);
        bool memeJour = (date1[0] == date2[0]);

        if (date1[2] < date2[2]){
            estAnterieure = true;
        }
        else if (memeAnnee && date1[1] < date2[1]){
            estAnterieure = true;
        }
        else if (memeAnnee && memeMois && date1[0] < date2[0]){
            estAnterieure = true;
        }
        else if (memeAnnee && memeMois && memeJour){
            estAnterieure = true;
        }

        return estAnterieure;
    }

    static bool horaireAnterieur(uint[] h1, uint[] h2){
        // Renvoie true si h1 < h2, sinon false
        bool estAnterieur = false;
        bool memeHeure = (h1[0] == h2[0]);

        if (h1[0] < h2[0]){
            estAnterieur = true;
        }
        else if (memeHeure && h1[1] < h2[1]){
            estAnterieur = true;
        }

        return estAnterieur;
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
