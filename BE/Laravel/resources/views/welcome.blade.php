<form action=" {{ route('forgot') }} " method="post">
    @csrf
    <input type="text" name="email" id="">
    <input type="submit" value="Submit">
</form>

