<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Laravel\Scout\Searchable;

class Post extends Model
{
    protected $table = 'post';
    protected $primaryKey = 'Id';
    const SEARCHABLE_FIELDS = ['Caption', 'Detail'];

    use HasFactory, Searchable;

    public function toSearchableArray()
    {
        return $this->only(self::SEARCHABLE_FIELDS);
    }

    public function searchableAs()
    {
        return 'PixelPalette_post';
    }
}
