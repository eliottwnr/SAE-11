// URL de base pour l'API, elle sera forgé par la suite pour obtenir les bonnes données
const API = "https://can.iutrs.unistra.fr/api";

// On attend que le DOM (document object model, donc ce qui relie les pages HTML au script) soit chargé pour lancer le script
document.addEventListener("DOMContentLoaded", () => {
    chargerCartes();
});

async function chargerCartes() {
    // On récupère l'ID de la réservation passé dans l'URL (ex: ?id=12)
    const params = new URLSearchParams(window.location.search);
    const resId = params.get("id");

    if (!resId) {
        document.getElementById("etat-vide").style.display = "block";
        return; 
    }

    // Remplit le champ de saisie avec l'id pour que l'utilisateur sache ce qu'il regarde
    const input = document.getElementById("resId");
    if (input) {
        input.value = resId;
    }

    try {
        // Récup des infos globales de la réservation (ports, date, nb de gens, etc.)
        const rep = await fetch(API + "/reservation/" + resId);
        const reservation = await rep.json();

        // Zone passager et véhicule (séparés) pour l'affichage
        const zonePassagers = document.getElementById("hote-passagers");
        const zoneVehicules = document.getElementById("hote-vehicules");
        
        // le template HTML qu'on va cloner pour chaque carte
        const template = document.getElementById("modele-carte");

        // Vider les zones avant de générer pour éviter de cumuler si on recharge
        zonePassagers.innerHTML = "";
        zoneVehicules.innerHTML = "";

        // Boucle pour créer chaque carte passager une par une
        for (let num = 1; num <= reservation.nbPassagers; num++) {
            // On récupère les détails spécifiques du passager (nom, prénom, catégorie)
            const repPassager = await fetch(
                API + "/reservation/" + resId + "/passager/" + num
            );
            const passager = await repPassager.json();

            // On clone le template et on injecte les données récupérées
            const carte = creerCarte(template);
            remplirCarte(carte, {
                liaison: reservation.portDepart + " > " + reservation.portArrivee,
                date: reservation.date,
                horaire: reservation.heure,
                code: passager.libelleCategorie,
                nom: passager.prenom + " " + passager.nom,
                qte: 1 // Un passager = une carte
            });

            // On ajoute la carte finie dans la zone des passagers
            zonePassagers.appendChild(carte);
        }

        // Même chose ici mais pour les véhicules
        for (let num = 1; num <= reservation.nbVehicules; num++) {
            // Récup des infos du véhicule (type, dimensions...)
            const repVehicule = await fetch(
                API + "/reservation/" + resId + "/vehicule/" + num
            );
            const vehicule = await repVehicule.json();

            const carte = creerCarte(template);
            remplirCarte(carte, {
                liaison: reservation.portDepart + " > " + reservation.portArrivee,
                date: reservation.date,
                horaire: reservation.heure,
                code: vehicule.libelle, 
                nom: reservation.nom, 
                qte: vehicule.quantite
            });

            zoneVehicules.appendChild(carte);
        }

        // Si on a des données, on cache le message "aucun résultat"
        document.getElementById("etat-vide").style.display = "none";

        // On affiche les sections passagers/véhicules seulement s'il y en a
        if (reservation.nbPassagers > 0) {
            document.getElementById("section-passagers").classList.remove("cache");
        }

        if (reservation.nbVehicules > 0) {
            document.getElementById("section-vehicules").classList.remove("cache");
        }

    } catch (e) {
        //  log si l'API répond pas ou si erreur 
        console.error("Erreur lors du chargement : ", e);
    }
}

// Fonction pour cloner le contenu du template <template>
function creerCarte(template) {
    return template.content.cloneNode(true);
}

// Remplit les données dans la carte en utilisant les attributs data-lier
function remplirCarte(carte, infos) {
    carte.querySelector('[data-lier="liaison"]').textContent = infos.liaison;
    carte.querySelector('[data-lier="date"]').textContent = infos.date;
    carte.querySelector('[data-lier="horaire"]').textContent = infos.horaire;
    carte.querySelector('[data-lier="code"]').textContent = infos.code;
    carte.querySelector('[data-lier="nom"]').textContent = infos.nom;
    carte.querySelector('[data-lier="qte"]').textContent = infos.qte;
}