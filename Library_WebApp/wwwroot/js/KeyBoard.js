window.initializeKeyboardNavigation = (tableId) => {
    document.addEventListener('keydown', (event) => {
        const keyCode = event.code;
        const key = event.key;
        switch (keyCode) {
            case 'ArrowUp':
                navigateTable(tableId, -1); 
                break;
            case 'ArrowDown':
                navigateTable(tableId, 1); 
                break;
            default:
                if (key.match(/^[a-z]$/i))
                {
                    highlightRows(tableId, key.toLowerCase());
                }
                break;
                break; 
        }
    });
};

function navigateTable(tableId, step) {
    const rows = document.querySelectorAll(`#${tableId} tbody tr`);
    if (!rows || rows.length === 0) return;

    let currentIndex = -1;
    rows.forEach((row, index) => {
        if (row.classList.contains('table-active')) {
            currentIndex = index;
            return;
        }
    });

    const newIndex = currentIndex + step;
    if (newIndex >= 0 && newIndex < rows.length) {
        rows[currentIndex]?.classList.remove('table-active');
        rows[newIndex]?.classList.add('table-active');
    }
}

function highlightRows(tableId, letter) {
    const rows = document.querySelectorAll(`#${tableId} tbody tr`);
    if (!rows || rows.length === 0) return;

    rows.forEach((row) => {
        const bookName = row.children[1].textContent.trim();
        const firstLetter = bookName.charAt(0).toLowerCase();
        if (firstLetter === letter.toLowerCase()) {
            row.classList.add('table-active');
        } else {
            row.classList.remove('table-active');
        }
    });
}
