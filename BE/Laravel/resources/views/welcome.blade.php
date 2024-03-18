<form action=" {{ route('login') }} " method="post">
    @csrf
    <input type="text" name="username" id="">
    <input type="text" name="password" id="">
    <input type="submit" value="Submit">
</form>
