export async function fetchWithAuth(url, options = {}) {
    const token = localStorage.getItem('token');
    console.log(token)
    const headers = {
        ...options.headers,
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json',
    };

    const response = await fetch(url, { ...options, headers });
    if (response.status === 401) {
        localStorage.removeItem('token');
        window.location.href = '/login';
        throw new Error('Não autorizado');
    }
    return response.json();
}
