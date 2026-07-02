document.getElementById('signupForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const name = document.getElementById('name').value;
    const email = document.getElementById('email').value;
    const phoneNumber = document.getElementById('phoneNumber').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
    const errorDiv = document.getElementById('error-message');

    errorDiv.style.display = 'none';
    errorDiv.textContent = '';

    if (password !== confirmPassword) {
        errorDiv.textContent = 'كلمتا المرور غير متطابقتين!';
        errorDiv.style.display = 'block';
        return;
    }

    fetch('/signup', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            name: name,
            email: email,
            phoneNumber: phoneNumber,
            password: password,
            confirmPassword: confirmPassword
        })
    })
    .then(response => response.json())
    .then(data => {
        if (data.success) {
            alert('تم إنشاء الحساب بنجاح!');
            window.location.href = '/login';
        } else {
            errorDiv.textContent = data.message || 'حدث خطأ أثناء إنشاء الحساب.';
            errorDiv.style.display = 'block';
        }
    })
    .catch(error => {
        console.error('Error:', error);
        errorDiv.textContent = 'حدث خطأ غير متوقع. يرجى المحاولة لاحقًا.';
        errorDiv.style.display = 'block';
    });
});