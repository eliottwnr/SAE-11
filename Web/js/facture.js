// URL de base pour l'API, elle sera forgé par la suite pour obtenir les bonnes données
const API_BASE = "https://can.iutrs.unistra.fr/api";

/**
 * Fonction principale asynchrone pour récupérer les données 
 * et générer l'affichage de la facture.
 */
async function chargerFacture() {
    // Récupération de l'ID de réservation dans les paramètres de l'URL (ex: ?id=10)
    const params = new URLSearchParams(window.location.search);
    const resId = params.get('id');

    // si aucun ID n'est trouvé dans l'URL on arrête la fonction
    if (!resId) return;

    try {
        // Premier appel à l'API pour récupérer les données globales de la réservation
        // await est utilisé ici pour attendre la résolution de la promesse fetch
        const res = await fetch(`${API_BASE}/reservation/${resId}`);
        const reservation = await res.json(); // Conversion du corps de la réponse en objet JSON

        // Mise à jour des éléments de l'entête via les sélecteurs d'attributs data-lier
        document.querySelector('[data-lier="reservationId"]').textContent = reservation.id;
        document.querySelector('[data-lier="liaison"]').textContent = `${reservation.portDepart} - ${reservation.portArrivee}`;
        document.querySelector('[data-lier="date"]').textContent = reservation.date;
        document.querySelector('[data-lier="horaire"]').textContent = reservation.heure;

        // Initialisation des variables de calcul pour le montant total
        let totalPassagers = 0;
        let totalVehicules = 0;

        // Gestion de la section Passagers
        const passagerContainer = document.querySelector('[data-role="passagers"] .tableau');
        
        // On boucle sur le nombre de passagers pour récupérer les détails de chacun
        for (let i = 1; i <= reservation.nbPassagers; i++) {
            // Requête spécifique pour chaque passager (on utilise l'index i pour l'ID passager)
            const pRes = await fetch(`${API_BASE}/reservation/${resId}/passager/${i}`);
            const p = await pRes.json();
            
            // Accumulation du prix dans le sous-total passagers
            totalPassagers += p.price;

            //Utilisation des "Template Literals" pour insérer des variables dans le HTML
            // .toFixed(2) permet de formater les nombres avec 2 décimales pour l'affichage monétaire (ex: 12.00€ et pas 12€ ou 12.1829€)
            passagerContainer.innerHTML += ` 
                <div class="ligne">
                    <div>P${i}</div>
                    <div>${p.libelleCategorie}</div>
                    <div>1</div>
                    <div>${p.price.toFixed(2)}€</div> 
                    <div>${p.price.toFixed(2)}€</div>
                </div>`;
        }

        // Gestion de la section Véhicules 
        const vehiculeContainer = document.querySelector('[data-role="vehicules"] .tableau');
        
        // Parcours de la liste des véhicules de la même manière que pour les passagers
        for (let i = 1; i <= reservation.nbVehicules; i++) {
            const vRes = await fetch(`${API_BASE}/reservation/${resId}/vehicule/${i}`);
            const v = await vRes.json();
            
            // Calcul du montant pour la ligne (prix unitaire x quantité)
            const ligneTotal = v.prix * v.quantite;
            totalVehicules += ligneTotal;

            // Nouvel utilisation des "Template Literals" pour insérer des variables dans le HTML
            vehiculeContainer.innerHTML += `
                <div class="ligne">
                    <div>${v.code}</div>
                    <div>${v.libelle}</div>
                    <div>${v.quantite}</div>
                    <div>${v.prix.toFixed(2)}€</div>
                    <div>${ligneTotal.toFixed(2)}€</div>
                </div>`;
        }

        // --- Affichage des totaux ---
        // Mise à jour des montants calculés dans le récapitulatif final
        document.querySelector('[data-lier="subtotalPassagers"]').textContent = `${totalPassagers.toFixed(2)} €`;
        document.querySelector('[data-lier="subtotalVehicules"]').textContent = `${totalVehicules.toFixed(2)} €`;
        
        // Calcul du prix total TTC (Somme des deux sous-totaux)
        const grandTotal = totalPassagers + totalVehicules;
        document.querySelector('[data-lier="total"]').textContent = `${grandTotal.toFixed(2)} €`;

    } catch (e) { 
        // Gestion des erreurs (ex: problème réseau ou erreur de l'API)
        console.error("Erreur lors du traitement de la facture :", e); 
    }
}

// Lancement de la fonction au chargement du script
chargerFacture();