using System;

// S'occupe de tout l'affichage 
partial class Programme {
    static void afficherPrompt(){
        Console.Write("--> ");
    }

    static void afficherSaisieIncorrecte(){
        Console.WriteLine("Saisie incorrecte !");
    }

    static void AfficherVehiculeDansListe(){
        Console.WriteLine("Le type de véhicule choisi à déjà été saisi !");
    }

    static void afficherLiaisons(){
        Console.Clear(); // nettoie la console
        Console.WriteLine("-- Choix de la liaison --\n\n");

        Console.WriteLine("   Départ\t-\tArrivée\n");
        Console.WriteLine("1. Groix\t-\tLorient");
        Console.WriteLine("2. Lorient\t-\tGroix");
        Console.WriteLine("3. Le Palais\t-\tQuiberon");
        Console.WriteLine("4. Quiberon\t-\tLe Palais");
    }

    // affiche toutes les horaires du jour correspondant 
    static void afficherHoraires(string[] horaires){
        int nbHoraires;
        uint i;

        Console.Clear(); // nettoie la console
        Console.WriteLine("-- Choix de l'horaire --\n\n");

        nbHoraires = horaires.Length;
        for (i = 0; i < nbHoraires; i++){
            Console.WriteLine((i + 1) + ". " + horaires[i]); // (i + 1) car l'indice commence à 0 et la liste à 1
        }
    }

    static void afficherTraversee(Reservation traversee){
        Console.Clear();
        Console.WriteLine("-- Résumé de la traversée --\n\n");

        Console.Write("Liaison : ");
        switch (traversee.idLiaison){
            case 1: // Liaison.groix_lorient
                Console.WriteLine("Groix - Lorient");
                break;

            case 2: // Liaison.lorient_groix
                Console.WriteLine("Lorient - Groix");
                break;

            case 3: // Liaison.quiberon_lepalais
                Console.WriteLine("Quiberon - Le Palais");
                break;

            case 4: // Liaison.lepalais_quiberon
                Console.WriteLine("Le Palais - Quiberon");
                break;

            default: // rien besoin de mettre puisque traversee.liaison fait partie d'une énumération
                break;
        }

        Console.WriteLine($"Date : {traversee.date[0]}/{traversee.date[1]}/{traversee.date[2]}"); // '$' permet de formater la chaîne
        Console.WriteLine($"Heure : {traversee.heure[0]}:{traversee.heure[1]}");
    }

    static void afficherCategoriesPassager(){
        Console.WriteLine("\nSaisir votre catégorie\n");
        Console.WriteLine("1. Adulte 26 ans et plus");
        Console.WriteLine("2. Jeune 18-25 ans");
        Console.WriteLine("3. Enfant 4-17 ans");
        Console.WriteLine("4. Bébé -4 ans");
        Console.WriteLine("5. Animal de compagnie");
    }

    static void afficherCategoriesVehicule(){
        Console.WriteLine("\nSaisir votre catégorie\n");
        Console.WriteLine("1. Trottinette électrique");
        Console.WriteLine("2. Vélo ou remorque à vélo");
        Console.WriteLine("3. Vélo électrique");
        Console.WriteLine("4. Vélo cargo ou tandem");
        Console.WriteLine("5. Deux-roues <= 125cm³");
        Console.WriteLine("6. Deux-roues > 125cm³");
        Console.WriteLine("7. Voiture moins de 4m");
        Console.WriteLine("8. Voiture de 4m à 4,39m");
        Console.WriteLine("9. Voiture de 4,40m à 4,79m");
        Console.WriteLine("10. Voiture de 4,80m et plus");
        Console.WriteLine("11. Camping-car ou véhicule de plus de 2,10m de haut");
    }

    static void afficherPrixTotal(double prix){
        Console.Clear();
        Console.WriteLine("-- Prix total --\n\n");
        Console.WriteLine($"{prix.ToString("#.##")}€"); // affiche le prix avec maximum 2 chiffres après la virgule 
    }

    static void afficherDateNonAnterieure(){
        Console.WriteLine("La date saisie n'est pas antérieure à la date du trajet aller !");
    }

    static void afficherHoraireNonAnterieure(){
        Console.WriteLine("L'horaire saisie n'est pas antérieure à l'horaire du trajet aller !");
    }
}
