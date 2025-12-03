let cartesActuelles = [];
let indexCarte = 0;

function getNomLiaison(idLiaison) {
    const liaisons = {
        1: 'Lorient - Groix',
        2: 'Groix - Lorient',
        3: 'Quiberon - Le Palais',
        4: 'Le Palais - Quiberon'
    };
    return liaisons[idLiaison] || 'Inconnue';
}

function getNomBateau(idLiaison) {
    return (idLiaison === 1 || idLiaison === 2) ? 'Breizh Nevez' : 'Vindilis';
}

function formatDate(dateStr) {
    const [annee, mois, jour] = dateStr.split('-');
    return `${jour}/${mois}/${annee}`;
}

function creerCartes(data) {
    const reservation = data.reservation;
    const passagers = data.passagers;
    const vehicules = data.vehicules;
    const cartes = [];

    passagers.forEach((p, i) => {
        const carte = {
            type: 'passager',
            nom: p.nom,
            prenom: p.prenom,
            categorie: p.categorie,
            liaison: getNomLiaison(reservation.idLiaison),
            date: formatDate(reservation.date),
            heure: reservation.heure,
            bateau: getNomBateau(reservation.idLiaison),
        };
        cartes.push(carte);
    });

    if (vehicules && vehicules.length > 0) {
        vehicules.forEach(v => {
            for (let i = 0; i < v.quantite; i++) {
                const carte = {
                    type: 'vehicule',
                    categorie: v.codeCategorie,
                    liaison: getNomLiaison(reservation.idLiaison),
                    date: formatDate(reservation.date),
                    heure: reservation.heure,
                    bateau: getNomBateau(reservation.idLiaison),
                };
                cartes.push(carte);
            }
        });
    }

    return cartes;
}

function afficherCarte(index) {
    const container = document.getElementById('cartes-container');

    if (cartesActuelles.length === 0) {
        container.innerHTML = '<p>Aucune carte à afficher</p>';
        return;
    }

    const carte = cartesActuelles[index];

    let html = '';
    if (carte.type === 'passager') {
        html = `
            <div class="carte-embarquement">
                <h2>Carte d'embarquement - Passager</h2>
                <div class="carte-info">
                    <p><strong>Nom:</strong> ${carte.nom}</p>
                    <p><strong>Prénom:</strong> ${carte.prenom}</p>
                    <p><strong>Catégorie:</strong> ${carte.categorie}</p>
                    <p><strong>Liaison:</strong> ${carte.liaison}</p>
                    <p><strong>Date:</strong> ${carte.date}</p>
                    <p><strong>Heure de départ:</strong> ${carte.heure}</p>
                    <p><strong>Bateau:</strong> ${carte.bateau}</p>
                </div>
            </div>
        `;
    } else {
        html = `
            <div class="carte-embarquement">
                <h2>Carte d'embarquement - Véhicule</h2>
                <div class="carte-info">
                    <p><strong>Type:</strong> ${carte.categorie}</p>
                    <p><strong>Liaison:</strong> ${carte.liaison}</p>
                    <p><strong>Date:</strong> ${carte.date}</p>
                    <p><strong>Heure de départ:</strong> ${carte.heure}</p>
                    <p><strong>Bateau:</strong> ${carte.bateau}</p>
                </div>
            </div>
        `;
    }

    container.innerHTML = html;

    document.getElementById('compteur-cartes').textContent = `Carte ${index + 1} / ${cartesActuelles.length}`;

    document.getElementById('btn-precedent').disabled = (index === 0);
    document.getElementById('btn-suivant').disabled = (index === cartesActuelles.length - 1);
}

function cartePrecedente() {
    if (indexCarte > 0) {
        indexCarte--;
        afficherCarte(indexCarte);
    }
}

function carteSuivante() {
    if (indexCarte < cartesActuelles.length - 1) {
        indexCarte++;
        afficherCarte(indexCarte);
    }
}

fetch('./reservation.json')
    .then(response => response.json())
    .then(reservations => {
        reservations.forEach(item => {
            const cartes = creerCartes(item);
            cartesActuelles = cartesActuelles.concat(cartes);
        });

        if (cartesActuelles.length > 0) {
            indexCarte = 0;
            afficherCarte(indexCarte);
        } else {
            document.getElementById('cartes-container').innerHTML = '<p>Aucune carte trouvée</p>';
        }
    })
    .catch(err => {
        console.error('Erreur:', err);
        document.getElementById('cartes-container').textContent = 'Erreur de chargement de la réservation';
    });

document.addEventListener('DOMContentLoaded', function() {
    const btnPrev = document.getElementById('btn-precedent');
    const btnNext = document.getElementById('btn-suivant');

    if (btnPrev) btnPrev.addEventListener('click', cartePrecedente);
    if (btnNext) btnNext.addEventListener('click', carteSuivante);
});