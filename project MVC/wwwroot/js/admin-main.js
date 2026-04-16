// admin-main.js

document.addEventListener('DOMContentLoaded', () => {
    localStorage.setItem('admin_site_logged_in', 'true');
    // === Session Dummy Check ===
    const isLoginPage = document.getElementById('loginForm') !== null;
    const isLoggedIn = localStorage.getItem('admin_site_logged_in') === 'true';
    if (isLoggedIn && isLoginPage) {
        window.location.href = '/admin/dashboard';
    } else if (!isLoggedIn && !isLoginPage) {
        window.location.href = '/admin';
    }

    // === Login Page Logic ===
    const loginForm = document.getElementById('loginForm');
    const loginError = document.getElementById('loginError');

    if (loginForm) {
        loginForm.addEventListener('submit', (e) => {
            e.preventDefault();
            const username = document.getElementById('username').value;
            const password = document.getElementById('password').value;

            if (username === 'admin' && password === 'admin') {
                localStorage.setItem('admin_site_logged_in', 'true');
                window.location.href = '/admin/dashboard';
            } else {
                loginError.style.display = 'block';
                setTimeout(() => { loginError.style.display = 'none'; }, 3000);
            }
        });
    }

    // === Dashboard Logic ===
    if (!isLoginPage) {
        initDashboard();
    }
});

function initDashboard() {
    // Logout
    document.querySelector('.logout-btn').addEventListener('click', (e) => {
        e.preventDefault();
        localStorage.removeItem('admin_site_logged_in');
        window.location.href = '/admin';
    });

    // Sidebar Toggle
    const toggleSidebarBtn = document.getElementById('toggleSidebar');
    const sidebar = document.getElementById('sidebar');
    if (toggleSidebarBtn && sidebar) {
        toggleSidebarBtn.addEventListener('click', () => { sidebar.classList.toggle('show'); });
        document.addEventListener('click', (e) => {
            if (window.innerWidth <= 768 && !sidebar.contains(e.target) && !toggleSidebarBtn.contains(e.target)) {
                sidebar.classList.remove('show');
            }
        });
    }

    // Navigation highlight (optional, for responsive sidebar close)
    // Removed SPA tab logic


    // Dummy Data
    let posts = [
        { id: 1, title: 'Welcome to the New Website', author: 'Super Admin', status: 'Published', date: '2023-10-25' },
        { id: 2, title: 'Top 10 Fashion Trends', author: 'Editor 1', status: 'Published', date: '2023-11-02' },
        { id: 3, title: 'Upcoming Winter Collection', author: 'Super Admin', status: 'Draft', date: '2023-11-15' }
    ];

    let users = [
        { id: 1, name: 'Super Admin', email: 'admin@website.com', role: 'Admin' },
        { id: 2, name: 'Jane Doe', email: 'jane.editor@website.com', role: 'Editor' },
        { id: 3, name: 'John Smith', email: 'john@gmail.com', role: 'Subscriber' }
    ];

    // Stats Elements
    const statPosts = document.getElementById('stat-posts');
    const statUsers = document.getElementById('stat-users');
    const dashStatPosts = document.querySelector('.dash-stat-posts');
    const dashStatUsers = document.querySelector('.dash-stat-users');

    function updateStats() {
        if (statPosts) statPosts.textContent = posts.length;
        if (statUsers) statUsers.textContent = users.length;
        if (dashStatPosts) dashStatPosts.textContent = posts.length;
        if (dashStatUsers) dashStatUsers.textContent = users.length;
    }

    // Render Posts
    function renderPosts() {
        const tbody = document.getElementById('postsTableBody');
        if (!tbody) return;
        tbody.innerHTML = '';

        posts.forEach(p => {
            const statusClass = p.status === 'Draft' ? 'badge-warning' : 'badge-success';
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>#${p.id}</td>
                <td><strong>${p.title}</strong></td>
                <td>${p.author}</td>
                <td><span class="badge ${statusClass}">${p.status}</span></td>
                <td>${p.date}</td>
                <td>
                    <button class="btn-icon btn-edit" onclick="editPost(${p.id})"><i class="fa-solid fa-pen"></i></button>
                    <button class="btn-icon btn-delete" onclick="deletePost(${p.id})"><i class="fa-solid fa-trash"></i></button>
                </td>
            `;
            tbody.appendChild(tr);
        });
        updateStats();
    }

    // Render Users
    function renderUsers() {
        const tbody = document.getElementById('usersTableBody');
        if (!tbody) return;
        tbody.innerHTML = '';

        users.forEach(u => {
            let roleClass = 'badge-success';
            if (u.role === 'Admin') roleClass = 'badge-primary';
            else if (u.role === 'Editor') roleClass = 'badge-warning';

            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>#${u.id}</td>
                <td>
                    <div class="img-cell">
                        <img src="https://ui-avatars.com/api/?name=${encodeURIComponent(u.name)}&background=random" alt="${u.name}">
                        <span>${u.name}</span>
                    </div>
                </td>
                <td>${u.email}</td>
                <td><span class="badge ${roleClass}">${u.role}</span></td>
                <td>
                    <button class="btn-icon btn-edit" onclick="editUser(${u.id})"><i class="fa-solid fa-pen"></i></button>
                    <button class="btn-icon btn-delete" onclick="deleteUser(${u.id})"><i class="fa-solid fa-trash"></i></button>
                </td>
            `;
            tbody.appendChild(tr);
        });
        updateStats();
    }

    // Global Window Actions
    window.deletePost = function (id) {
        if (confirm(`Delete post #${id}?`)) {
            posts = posts.filter(p => p.id !== id);
            renderPosts();
        }
    };
    window.editPost = function (id) {
        const p = posts.find(p => p.id === id);
        if (!p) return;
        document.getElementById('postModalTitle').textContent = 'Edit Post';
        document.getElementById('postId').value = p.id;
        document.getElementById('postTitle').value = p.title;
        document.getElementById('postStatus').value = p.status;
        document.getElementById('postContent').value = "Sample Content..."; // default dummy
        document.getElementById('postModal').style.display = 'flex';
    };

    window.deleteUser = function (id) {
        if (id === 1) { alert("Cannot delete main admin"); return; }
        if (confirm(`Delete user #${id}?`)) {
            users = users.filter(u => u.id !== id);
            renderUsers();
        }
    };
    window.editUser = function (id) {
        const u = users.find(u => u.id === id);
        if (!u) return;
        document.getElementById('userModalTitle').textContent = 'Edit User';
        document.getElementById('userId').value = u.id;
        document.getElementById('userName').value = u.name;
        document.getElementById('userEmail').value = u.email;
        document.getElementById('userRole').value = u.role;
        document.getElementById('userModal').style.display = 'flex';
    };

    // Modal Logic
    const postModal = document.getElementById('postModal');
    const userModal = document.getElementById('userModal');

    document.getElementById('btnAddPost')?.addEventListener('click', () => {
        document.getElementById('postForm').reset();
        document.getElementById('postId').value = '';
        document.getElementById('postModalTitle').textContent = 'Add Post';
        postModal.style.display = 'flex';
    });
    document.getElementById('btnAddUser')?.addEventListener('click', () => {
        document.getElementById('userForm').reset();
        document.getElementById('userId').value = '';
        document.getElementById('userModalTitle').textContent = 'Add User';
        userModal.style.display = 'flex';
    });
    document.querySelectorAll('.close-modal').forEach(btn => {
        btn.addEventListener('click', function () {
            this.closest('.modal-overlay').style.display = 'none';
        });
    });

    // Form Submits
    document.getElementById('postForm')?.addEventListener('submit', function (e) {
        e.preventDefault();
        const id = document.getElementById('postId').value;
        const newPost = {
            id: id ? parseInt(id) : Math.floor(Math.random() * 1000) + 10,
            title: document.getElementById('postTitle').value,
            author: id ? posts.find(p => p.id == id).author : 'Super Admin',
            status: document.getElementById('postStatus').value,
            date: id ? posts.find(p => p.id == id).date : new Date().toISOString().split('T')[0]
        };

        if (id) {
            const index = posts.findIndex(p => p.id == id);
            posts[index] = newPost;
        } else {
            posts.unshift(newPost);
        }
        postModal.style.display = 'none';
        renderPosts();
    });

    document.getElementById('userForm')?.addEventListener('submit', function (e) {
        e.preventDefault();
        const id = document.getElementById('userId').value;
        const newUser = {
            id: id ? parseInt(id) : Math.floor(Math.random() * 1000) + 10,
            name: document.getElementById('userName').value,
            email: document.getElementById('userEmail').value,
            role: document.getElementById('userRole').value
        };

        if (id) {
            const index = users.findIndex(u => u.id == id);
            users[index] = newUser;
        } else {
            users.push(newUser);
        }
        userModal.style.display = 'none';
        renderUsers();
    });

    // Settings Submit
    document.getElementById('settingsGeneralForm')?.addEventListener('submit', function (e) {
        e.preventDefault();
        alert("General settings saved!");
    });
    document.getElementById('settingsAppearanceForm')?.addEventListener('submit', function (e) {
        e.preventDefault();
        alert("Appearance settings saved!");
    });

    // Initial Render
    renderPosts();
    renderUsers();
}
