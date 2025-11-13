using System;

// S'occupe de toutes les saisies utilisateur
partial class Programme {
    // Plus simple de saisir un entier non signé mais plus explicite de renvoyer une énumération
    static Liaison saisirLiaison(){
        uint liaison;
        
        string? saisie; // string? : string qui peut être null (au cas où aucune saisie)
        bool saisieJuste;

        Console.Clear(); // nettoie la console
        Console.WriteLine("-- Choix de la liaison --\n\n");

        Console.WriteLine("   Départ\t-\tArrivée\n");
        Console.WriteLine("1. Groix\t-\tLorient");
        Console.WriteLine("2. Lorient\t-\tGroix");
        Console.WriteLine("3. Le Palais\t-\tQuiberon");
        Console.WriteLine("4. Quiberon\t-\tLe Palais");

        do {
            Console.Write("--> "); // prompt
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out liaison);

            if (!saisieJuste || (liaison != 1 && liaison != 2 && liaison != 3 && liaison != 4)){
                Console.WriteLine("Saisie Incorrecte !");
            }
        } while (liaison != 1 && liaison != 2 && liaison != 3 && liaison != 4);

        return (Liaison) liaison; // cast (conversion) vers l'énumération
    }

    // permet de saisir des dates en novembre 2025 (mais modifiable)
    static uint[] saisirDate(){
        uint[] date = new uint[3]; // jour, mois, année

        string? saisie;
        bool saisieJuste;

        Console.Clear(); // nettoie la console
        do {
            Console.Write("Saisir le jour de la traversée : ");
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out date[0]);

            if (!saisieJuste || date[0] <= 0 || date[0] >= 31){
                Console.WriteLine("Saisie Incorrecte !");
            }
        } while (date[0] <= 0 || date[0] >= 31); // tant que le jour n'est pas entre 1 et 30 (inclus)

        do {
            Console.Write("Saisir le mois de la traversée : ");
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out date[1]);

            if (!saisieJuste || date[1] != 11){
                Console.WriteLine("Saisie Incorrecte !");
            }
        } while (date[1] != 11); // tant que le mois n'est pas en novembre 

        do {
            Console.Write("Saisir l'année de la traversée : ");
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out date[2]);

            if (!saisieJuste || date[2] != 2025){
                Console.WriteLine("Saisie Incorrecte !");
            }
        } while (date[2] != 2025); // tant que le mois n'est pas 2025 

        return date;
    }
}
