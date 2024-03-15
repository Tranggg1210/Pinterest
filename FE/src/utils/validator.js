function validateEmail(email) {
    if (email.trim() === '') {
        return false;
    } else if (!/^[a-zA-Z0-9_!#$%&'*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$/.test(email)) {
        return false;
    }
    return true;
}

export { validateEmail };