using System; 

// Programme principal
partial class Programme { // partial permet de séparer en plusieurs fichiers une même classe
    public static void Main(){
        uint[] date = saisirDate();
        string[] horairesDuJour;
        uint[] horaire;

        Liaison liaison = saisirLiaison();
        
        // récupération des horaires du jour et de la liaison choisis
        horairesJour(liaison, date[0], out horairesDuJour); // date[0] correspond au jour
        horaire = saisirHoraire(horairesDuJour);
    }
}
