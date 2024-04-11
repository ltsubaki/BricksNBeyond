// cookieConsent.js

// Function to check if the user has given consent
function checkConsent() {
    return localStorage.getItem('cookieConsent') === 'true';
}

// Function to show the cookie consent banner with a delay
function showConsentBannerWithDelay() {
    // Hide the banner initially
    document.getElementById('cookieConsentBanner').style.display = 'none';

    // Delay showing the banner
    setTimeout(function () {
        showConsentBanner();
    }, 3000); // 3000 milliseconds = 3 seconds
}

// Function to show the cookie consent banner
function showConsentBanner() {
    const consentBanner = document.getElementById('cookieConsentBanner');
    consentBanner.style.display = 'block';
}

// Function to handle user consent
function giveConsent() {
    localStorage.setItem('cookieConsent', 'true');
    document.getElementById('cookieConsentBanner').style.display = 'none';
}
function declineConsent() {
    // Optionally, you can add logic here to handle the decline action
    // For example, you could block certain features or services if the user declines consent
    // You can also remove any existing consent stored in localStorage
    localStorage.removeItem('cookieConsent');
    document.getElementById('cookieConsentBanner').style.display = 'none';
}