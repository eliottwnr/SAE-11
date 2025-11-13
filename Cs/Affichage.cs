using System;

partial class Programme {
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
}
