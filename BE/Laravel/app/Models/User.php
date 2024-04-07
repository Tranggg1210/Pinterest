<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Laravel\Scout\Searchable;

class User extends Model
{
    protected $table = 'user';
    protected $primaryKey = 'Id';
    public $timestamps = false;
    use HasFactory,Searchable;
    const SEARCHABLE_FIELDS = ['UserName', 'FirstName', 'LastName', 'Email', 'Country'];

    public function toSearchableArray()
    {
        return $this->only(self::SEARCHABLE_FIELDS);
    }

    public function searchableAs()
    {
        return 'PixelPalette_user';
    }
}

