using System; 

// Programme principal
partial class Programme { // partial permet de séparer en plusieurs fichiers une même classe
    public static void Main(){
        uint[] date = saisirDate();

        Liaison liaison = saisirLiaison();

        afficherHoraires(liaison, date[0]); // date[0] correspond au jour
    }
}
