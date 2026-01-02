const API = "https://can.iutrs.unistra.fr/api";

document.addEventListener("DOMContentLoaded", () => {
    chargerCartes();
});

async function chargerCartes() {
    const params = new URLSearchParams(window.location.search);
    const resId = params.get("id");

    // Remplit le champ de saisie avec l'id
    const input = document.getElementById("resId");
    if (input) {
        input.value = resId;
    }

    try {
        // Récup des infos de la réservations
        const rep = await fetch(API + "/reservation/" + resId);
        const reservation = await rep.json();
        // Zone passager et véhicule (séparés)
        const zonePassagers = document.getElementById("hote-passagers");
        const zoneVehicules = document.getElementById("hote-vehicules");
        // le template pour les cartes
        const template = document.getElementById("modele-carte");

        // Vider les zones pour créer les nouvelles cartes
        zonePassagers.innerHTML = "";
        zoneVehicules.innerHTML = "";
        // Création de la carte passager
        for (let num = 1; num <= reservation.nbPassagers; num++) {
            const repPassager = await fetch(
                API + "/reservation/" + resId + "/passager/" + num
            );
            const passager = await repPassager.json();

            const carte = creerCarte(template);
            remplirCarte(carte, {
                liaison: reservation.portDepart + " > " + reservation.portArrivee,
                date: reservation.date,
                horaire: reservation.heure,
                code: passager.libelleCategorie,
                nom: passager.prenom + " " + passager.nom,
                qte: 1
            });

            zonePassagers.appendChild(carte);
        }
        // Même chose ici mais pour les véhicules
        for (let num = 1; num <= reservation.nbVehicules; num++) {
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

        document.getElementById("etat-vide").style.display = "none";

        if (reservation.nbPassagers > 0) {
            document.getElementById("section-passagers").classList.remove("cache");
        }

        if (reservation.nbVehicules > 0) {
            document.getElementById("section-vehicules").classList.remove("cache");
        }

    } catch (e) {
        console.error("Erreur lors du chargement :", e);
    }
}

function creerCarte(template) {
    return template.content.cloneNode(true);
}

// Remplit les données dans la carte
function remplirCarte(carte, infos) {
    carte.querySelector('[data-lier="liaison"]').textContent = infos.liaison;
    carte.querySelector('[data-lier="date"]').textContent = infos.date;
    carte.querySelector('[data-lier="horaire"]').textContent = infos.horaire;
    carte.querySelector('[data-lier="code"]').textContent = infos.code;
    carte.querySelector('[data-lier="nom"]').textContent = infos.nom;
    carte.querySelector('[data-lier="qte"]').textContent = infos.qte;
}
