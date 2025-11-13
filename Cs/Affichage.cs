using System;

// S'occupe de tout l'affichage 
partial class Programme {
    static void afficherLiaison(Liaison liaison){
        switch (liaison){
            case Liaison.lorient_groix: 
                Console.WriteLine("Lorient - Groix");
                break;
                
            case Liaison.groix_lorient:
                Console.WriteLine("Groix - Lorient");
                break;

            case Liaison.quiberon_lepalais: 
                Console.WriteLine("Quiberon - Le Palais");
                break;

            case Liaison.lepalais_quiberon: 
                Console.WriteLine("Le Palais - Quiberon");
                break;

            default: // rien besoin de mettre puisque liaison est forcément entre 1 et 4 (Vérifié lors de la saisie)
                break;
        }
    }

    // affiche toutes les horaires du jour correspondant 
    static void afficherHoraires(Liaison liaison, uint jour){
        string[] horaires;
        bool succes = horairesJour(liaison, jour, out horaires);

        Console.Write("Horaires du " + jour + " pour la liaison ");
        afficherLiaison(liaison);

        if (succes){
            foreach (string heure in horaires){
                Console.Write(heure + " ");
            }
            Console.WriteLine();
        }
    }
}
