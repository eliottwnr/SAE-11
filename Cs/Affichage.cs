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
}
