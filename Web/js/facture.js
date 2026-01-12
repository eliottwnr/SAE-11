const API_BASE = "https://can.iutrs.unistra.fr/api";

async function chargerFacture() {
    const params = new URLSearchParams(window.location.search);
    const resId = params.get('id');
    if (!resId) return;

    try {
        const res = await fetch(`${API_BASE}/reservation/${resId}`); // await : Attend de recevoir le résultat avant de continuer (système de promesse du js)
        const reservation = await res.json();

        // Remplir entête
        document.querySelector('[data-lier="reservationId"]').textContent = reservation.id;
        document.querySelector('[data-lier="liaison"]').textContent = `${reservation.portDepart} - ${reservation.portArrivee}`;
        document.querySelector('[data-lier="date"]').textContent = reservation.date;
        document.querySelector('[data-lier="horaire"]').textContent = reservation.heure;

        let totalPassagers = 0;
        let totalVehicules = 0;

        // Passagers
        const passagerContainer = document.querySelector('[data-role="passagers"] .tableau');
        for (let i = 1; i <= reservation.nbPassagers; i++) {
            const pRes = await fetch(`${API_BASE}/reservation/${resId}/passager/${i}`);
            const p = await pRes.json();
            totalPassagers += p.price;
            // toFixed(2) : 2 nombres après la virgule
            passagerContainer.innerHTML += ` 
                <div class="ligne">
                    <div>P${i}</div>
                    <div>${p.libelleCategorie}</div>
                    <div>1</div>
                    <div>${p.price.toFixed(2)}€</div> 
                    <div>${p.price.toFixed(2)}€</div>
                </div>`;
        }

        // Véhicules
        const vehiculeContainer = document.querySelector('[data-role="vehicules"] .tableau');
        for (let i = 1; i <= reservation.nbVehicules; i++) {
            const vRes = await fetch(`${API_BASE}/reservation/${resId}/vehicule/${i}`);
            const v = await vRes.json();
            const ligneTotal = v.prix * v.quantite;
            totalVehicules += ligneTotal;
            vehiculeContainer.innerHTML += `
                <div class="ligne">
                    <div>${v.code}</div>
                    <div>${v.libelle}</div>
                    <div>${v.quantite}</div>
                    <div>${v.prix.toFixed(2)}€</div>
                    <div>${ligneTotal.toFixed(2)}€</div>
                </div>`;
        }

        // Totaux
        document.querySelector('[data-lier="subtotalPassagers"]').textContent = `${totalPassagers.toFixed(2)} €`;
        document.querySelector('[data-lier="subtotalVehicules"]').textContent = `${totalVehicules.toFixed(2)} €`;
        document.querySelector('[data-lier="total"]').textContent = `${(totalPassagers + totalVehicules).toFixed(2)} €`;

    } catch (e) { console.error(e); }
}

chargerFacture();