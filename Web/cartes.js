fetch('reservation.json')
  .then(response => response.json())
  .then(reservation => {
    afficherCartes(reservation);
  })
  .catch(err => {
    document.getElementById('cartes-container').textContent = 'Erreur de chargement du fichier JSON.';
  });

function afficherCartes(data) {
  const container = document.getElementById('cartes-container');
  container.innerHTML = '';
  const liaison = getNomLiaison(data.idLiaison);
  data.passagers.forEach((p, i) => {
    const card = document.createElement('div');
    card.className = 'carte-embarquement';
    card.innerHTML = `
      <h2>Carte d'embarquement #${i + 1}</h2>
      <ul>
        <li><strong>Nom :</strong> ${p.nom}</li>
        <li><strong>Prénom :</strong> ${p.prenom}</li>
        <li><strong>Catégorie :</strong> ${getCategorieLibelle(p.codeCategorie)}</li>
        <li><strong>Liaison :</strong> ${liaison}</li>
        <li><strong>Date :</strong> ${formatDate(data.date)}</li>
        <li><strong>Heure :</strong> ${formatHeure(data.heure)}</li>
      </ul>
    `;
    container.appendChild(card);
  });
}

function getNomLiaison(id) {
  switch (id) {
    case 1: return "Lorient → Groix";
    case 2: return "Groix → Lorient";
    case 3: return "Quiberon → Le Palais";
    case 4: return "Le Palais → Quiberon";
    default: return "?";
  }
}
function getCategorieLibelle(c) {
  switch (c) {
    case 'adu26p': return 'Adulte (≥ 26 ans)';
    case 'jeu1825': return 'Jeune (18-25 ans)';
    case 'enf417': return 'Enfant (4-17 ans)';
    case 'bebe': return 'Bébé (< 4 ans)';
    case 'ancomp': return 'Animal compagnie';
    default: return c;
  }
}
function formatDate(s) {
  return s.replace(/(\d{2})(\d{2})(\d{4})/, "$1/$2/$3");
}
function formatHeure(h) {
  return h.slice(0,2) + ":" + h.slice(2,4);
}
