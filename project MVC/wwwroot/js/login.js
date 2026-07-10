document.getElementById('loginForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    const data = new FormData();
    data.append('email', email);
    data.append('password', password);

    fetch('/login', {
        method: 'POST',
        body: data
    })
    .then(async res => {
        if (res.redirected) {
            window.location.href = res.url;
        } else if (res.ok) {
            window.location.href = "/";
        } else {
            const errorText = await res.text();
            alert(errorText || 'البريد الإلكتروني أو كلمة المرور غير صحيحة!');
        }
    })
    .catch(() => {
        alert('حدث خطأ أثناء الاتصال بالخادم. يرجى المحاولة مرة أخرى.');
    });
});