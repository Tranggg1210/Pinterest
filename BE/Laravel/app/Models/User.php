<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Laravel\Scout\Searchable;
use Tymon\JWTAuth\Contracts\JWTSubject;
use Illuminate\Notifications\Notifiable;
use Illuminate\Foundation\Auth\User as Authenticatable;
use Laravel\Sanctum\HasApiTokens;

class User extends Authenticatable implements JWTSubject
{
    protected $table = 'User';
    protected $primaryKey = 'Id';
    public $timestamps = false;
    // protected $fillable = 
    use HasFactory,Searchable,HasApiTokens  ;
    const SEARCHABLE_FIELDS = ['UserName', 'FirstName', 'LastName', 'Email', 'Country'];

    public function getJWTIdentifier()
    {
        return $this->getKey();
    }
    public function getJWTCustomClaims()
    {
        return [];
    }
    public function searchableAs()
    {
        return 'PixelPalette_user';
    }
}

