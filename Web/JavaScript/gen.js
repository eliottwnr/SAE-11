// Chemin relatif vers le JSON dans le dossier data
const url = 'Cs/js/reservation.json';

async function loadReservations() {
  try {
    const res = await fetch(url, { cache: "no-store" });
    if (!res.ok) throw new Error(`HTTP ${res.status} - ${res.statusText}`);
    const json = await res.json();
    render(json);
  } catch (err) {
    document.getElementById('output').textContent = 'Erreur: ' + err.message;
    console.error(err);
  }
}

function render(data) {
  const out = document.getElementById('output');
  out.innerHTML = '';
  if (!data || !data.reservations || data.reservations.length === 0) {
    out.textContent = 'Aucune réservation trouvée.';
    return;
  }

  data.reservations.forEach(r => {
    const card = document.createElement('div');
    card.className = 'card';
    card.innerHTML = `
      <div class="title">${r.nom} — Liaison ${r.idLiaison}</div>
      <div>Date : ${r.date} · Départ : ${r.heure}</div>
      <div style="margin-top:8px"><strong>Passagers :</strong> ${r.passagers.length}</div>
      <div><strong>Véhicules :</strong> ${r.vehicules.map(v => v.codeCategorie + '×' + v.quantite).join(', ')}</div>
    `;
    out.appendChild(card);
  });
}

loadReservations();
