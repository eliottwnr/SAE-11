using System; 

class Reservations {
    struct Traversee {
        public uint liaison;
        public uint[] date;
        public uint[] heure;

        public Traversee(uint l){
            liaison = l;

            date = new uint[3]; // Jour, Mois, Année 
            heure = new uint[2]; // Heures, Minutes
        }
    }

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

    static void afficherLiaison(uint liaison){
        switch (liaison){
            case 1: 
                Console.WriteLine("   Lorient\t-\tGroix");
                break;
                
            case 2:
                Console.WriteLine("   Groix\t-\tLorient");
                break;

            case 3: 
                Console.WriteLine("   Quiberon\t-\tLe Palais");
                break;

            case 4: 
                Console.WriteLine("   Le Palais\t-\tQuiberon");
                break;

            default: // rien besoin de mettre puisque liaison est forcément entre 1 et 4 (Vérifié lors de la saisie)
                break;
        }
    }

    public static void Main(){
        afficherLiaison(saisirLiaison());
    }
}
