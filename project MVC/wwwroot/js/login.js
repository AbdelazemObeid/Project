document.getElementById('loginForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const errorDiv = document.getElementById('error-message');

    errorDiv.style.display = 'none';
    errorDiv.textContent = '';

    fetch('/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email: email, password: password })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            if (data.role === 'Admin') {
                window.location.href = '/admin/dashboard';
            } else {
                window.location.href = '/';
            }
        } else {
            errorDiv.textContent = data.message || 'البريد الإلكتروني أو كلمة المرور غير صحيحة!';
            errorDiv.style.display = 'block';
        }
    })
    .catch(error => {
        console.error('Error:', error);
        errorDiv.textContent = 'حدث خطأ غير متوقع. يرجى المحاولة لاحقًا.';
        errorDiv.style.display = 'block';
    });
});