document.getElementById('loginForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    // مثال تحقق مؤقت (يمكن الربط بالباك إند لاحقًا)
    if(email === "test@example.com" && password === "123456") {
        alert('تم تسجيل الدخول بنجاح!');
        window.location.href = "dashboard.html"; // مثال على صفحة بعد تسجيل الدخول
    } else {
        alert('البريد الإلكتروني أو كلمة المرور غير صحيحة!');
    }
});