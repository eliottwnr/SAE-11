using System; 

// Programme principal
partial class Programme { // partial permet de séparer en plusieurs fichiers une même classe
    public static void Main(){
        string[] horairesDuJour;

        Liaison liaison = saisirLiaison();

        Traversee traversee = new Traversee(liaison);
        
        traversee.date = saisirDate();

        // récupération des horaires du jour et de la liaison choisis
        horairesJour(liaison, traversee.date[0], out horairesDuJour); // date[0] correspond au jour
        traversee.heure = saisirHoraire(horairesDuJour);
    }
}
