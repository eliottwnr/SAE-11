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
}
