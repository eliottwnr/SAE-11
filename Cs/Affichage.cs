using System;

// S'occupe de tout l'affichage 
partial class Programme {
    static void afficherLiaison(Liaison liaison){
        switch (liaison){
            case Liaison.lorient_groix: 
                Console.WriteLine("   Lorient\t-\tGroix");
                break;
                
            case Liaison.groix_lorient:
                Console.WriteLine("   Groix\t-\tLorient");
                break;

            case Liaison.quiberon_lepalais: 
                Console.WriteLine("   Quiberon\t-\tLe Palais");
                break;

            case Liaison.lepalais_quiberon: 
                Console.WriteLine("   Le Palais\t-\tQuiberon");
                break;

            default: // rien besoin de mettre puisque liaison est forcément entre 1 et 4 (Vérifié lors de la saisie)
                break;
        }
    }
}
