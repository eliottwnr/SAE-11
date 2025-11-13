using System;

// S'occupe de toutes les saisies utilisateur
partial class Programme {
    static uint saisirLiaison(){
        uint liaison;
        
        string? saisie; // string? : string qui peut être null (au cas où aucune saisie)
        bool saisieJuste;

        Console.Clear();
        Console.WriteLine("-- Choix de la liaison --\n\n");

        Console.WriteLine("   Départ\t-\tArrivée\n");
        Console.WriteLine("1. Lorient\t-\tGroix");
        Console.WriteLine("2. Groix\t-\tLorient");
        Console.WriteLine("3. Quiberon\t-\tLe Palais");
        Console.WriteLine("4. Le Palais\t-\tQuiberon");

        do {
            Console.Write("--> ");
            saisie = Console.ReadLine();
            saisieJuste = uint.TryParse(saisie, out liaison);

            if (!saisieJuste || (liaison != 1 && liaison != 2 && liaison != 3 && liaison != 4)){
                Console.WriteLine("Saisie Incorrecte !");
            }
        } while (liaison != 1 && liaison != 2 && liaison != 3 && liaison != 4);

        return liaison;
    }
}
