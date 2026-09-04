const checkboxes = document.querySelectorAll('.row-select');
const countLabel = document.getElementById('select-count');  // matches span's id

function updateCount() {
    const checkedCount = document.querySelectorAll('.row-select:checked').length;
    countLabel.textContent = checkedCount;  // just the number
}

checkboxes.forEach(cb => cb.addEventListener('change', updateCount));