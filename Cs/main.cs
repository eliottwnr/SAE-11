using System; 

// Programme principal
partial class Programme { // partial permet de séparer en plusieurs fichiers une même classe
    public static void Main(){
        Liaison liaison = saisirLiaison();

        afficherHoraires(liaison, 1);
    }
}
