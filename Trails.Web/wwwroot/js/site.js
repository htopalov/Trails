window.addEventListener('DOMContentLoaded', event => {

    //create beacon generate key call to endpoint to retrieve rnd key
    document.getElementById('gen-btn').addEventListener('click', async () => {
        let keyField = document.getElementById('key-field');
        let infoLabel = document.getElementById('info-label');
        let response = await fetch('/Administration/Beacon/getkey');
        if (response.ok) {
            let result = await response.json();
            keyField.value = result.key;
        } else {
            infoLabel.textContent = 'Error getting key from server! Type strong key by yourself or try again later...';
        }
    });

    //create beacon copy key 
    document.getElementById('copy-key').addEventListener('click', () => {
        navigator.clipboard.writeText(document.getElementById("key-field").value);
        document.getElementById('info-label').textContent = 'Copied!';
    });

    // Navbar shrink function
    var navbarShrink = function () {
        const navbarCollapsible = document.body.querySelector('#mainNav');
        if (!navbarCollapsible) {
            return;
        }
        if (window.scrollY === 0) {
            navbarCollapsible.classList.remove('navbar-shrink')
        } else {
            navbarCollapsible.classList.add('navbar-shrink')
        }

    };

    // Shrink the navbar 
    navbarShrink();

    // Shrink the navbar when page is scrolled
    document.addEventListener('scroll', navbarShrink);

    // Activate Bootstrap scrollspy on the main nav element
    const mainNav = document.body.querySelector('#mainNav');
    if (mainNav) {
        new bootstrap.ScrollSpy(document.body, {
            target: '#mainNav',
            offset: 72,
        });
    };

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });

});