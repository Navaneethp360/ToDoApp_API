const token = localStorage.getItem('token');
const apiUrl = 'https://localhost:7032/api/todo';

if (!token) {
    window.location.href = 'index.html';
}

const todoList = document.getElementById('todoList');
const addForm = document.getElementById('addTodoForm');
const newTodo = document.getElementById('newTodo');
const addStatus = document.getElementById('addStatus');
const logoutBtn = document.getElementById('logoutBtn');

// 🧠 Load todos on page load
document.addEventListener('DOMContentLoaded', loadTodos);

async function loadTodos() {
    try {
        const res = await fetch(apiUrl, {
            headers: { Authorization: 'Bearer ' + token }
        });

        if (!res.ok) throw new Error('Failed to load todos.');

        const todos = await res.json();
        todoList.innerHTML = ''; // clear existing

        todos.forEach(todo => {
            const li = document.createElement('li');

            const checkbox = document.createElement('input');
            checkbox.type = 'checkbox';
            checkbox.checked = todo.isCompleted;
            checkbox.addEventListener('change', () => toggleComplete(todo));

            const span = document.createElement('span');
            span.textContent = todo.title;
            if (todo.isCompleted) {
                span.style.textDecoration = 'line-through';
                span.style.color = 'gray';
            }

            const deleteBtn = document.createElement('button');
            deleteBtn.textContent = '🗑️';
            deleteBtn.style.marginLeft = '10px';
            deleteBtn.addEventListener('click', () => deleteTodo(todo.id));

            li.appendChild(checkbox);
            li.appendChild(span);
            li.appendChild(deleteBtn);
            todoList.appendChild(li);
        });

    } catch (err) {
        alert(err.message);
    }
}

// ➕ Add new todo
addForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const title = newTodo.value.trim();
    if (!title) return;

    try {
        const res = await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: 'Bearer ' + token
            },
            body: JSON.stringify({ title })
        });

        if (!res.ok) throw new Error('Failed to add todo');

        newTodo.value = '';
        addStatus.textContent = 'Task added successfully!';
        loadTodos();
    } catch (err) {
        addStatus.textContent = 'Failed to add task.';
    }

    setTimeout(() => addStatus.textContent = '', 3000);
});

// ✅ Toggle completion
async function toggleComplete(todo) {
    try {
        const res = await fetch(`${apiUrl}/${todo.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                Authorization: 'Bearer ' + token
            },
            body: JSON.stringify({
                title: todo.title,
                isCompleted: !todo.isCompleted
            })
        });

        if (!res.ok) throw new Error('Failed to update todo');
        loadTodos();
    } catch (err) {
        alert(err.message);
    }
}

// ❌ Delete task
async function deleteTodo(id) {
    try {
        const res = await fetch(`${apiUrl}/${id}`, {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + token
            }
        });

        if (!res.ok) throw new Error('Failed to delete todo');
        loadTodos();
    } catch (err) {
        alert(err.message);
    }
}

// 🚪 Logout
logoutBtn.addEventListener('click', () => {
    localStorage.removeItem('token');
    window.location.href = 'index.html';
});
