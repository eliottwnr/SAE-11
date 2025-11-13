using System;

// S'occupe de tout l'affichage 
partial class Programme {
    static void afficherPrompt(){
        Console.Write("--> ");
    }

    static void afficherSaisieIncorrecte(){
        Console.WriteLine("Saisie incorrecte !");
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
            Console.WriteLine((i + 1) + ". " + horaires[i]);
        }
    }

    static void afficherTraversee(Traversee traversee){
        Console.Clear();
        Console.WriteLine("-- Résumé de la traversée --\n\n");

        Console.Write("Liaison : ");
        switch (traversee.liaison){
            case Liaison.groix_lorient:
                Console.WriteLine("Groix - Lorient");
                break;

            case Liaison.lorient_groix:
                Console.WriteLine("Lorient - Groix");
                break;

            case Liaison.quiberon_lepalais:
                Console.WriteLine("Quiberon - Le Palais");
                break;

            case Liaison.lepalais_quiberon:
                Console.WriteLine("Le Palais - Quiberon");
                break;

            default: // rien besoin de mettre puisque traversee.liaison fait partie d'une énumération
                break;
        }

        Console.WriteLine($"Date : {traversee.date[0]}/{traversee.date[1]}/{traversee.date[2]}"); // $ permet de formater la chaîne
        Console.WriteLine($"Heure : {traversee.heure[0]}:{traversee.heure[1]}");
    }
}
