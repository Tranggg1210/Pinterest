<?php

namespace App\Models;

use Illuminate\Auth\Middleware\Authenticate;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Laravel\Scout\Searchable;
class User extends Model
{
    protected $table = 'User';
    protected $primaryKey = 'Id';
    public $timestamps = false;
    use HasFactory,Searchable;
    const SEARCHABLE_FIELDS = ['UserName', 'FirstName', 'LastName', 'Email', 'Country'];


    public function searchableAs()
    {
        return 'PixelPalette_user';
    }
}

