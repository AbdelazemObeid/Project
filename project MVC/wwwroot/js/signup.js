document.getElementById('signupForm').addEventListener('submit', function(e) {
    e.preventDefault();
    const name = document.getElementById('name').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    // مثال مؤقت لتسجيل حساب (يمكن الربط بالباك إند لاحقًا)
    alert(`تم إنشاء الحساب بنجاح!\nالاسم: ${name}\nالبريد: ${email}`);
    window.location.href = "login.html"; // تحويل المستخدم لتسجيل الدخول بعد إنشاء الحساب
});