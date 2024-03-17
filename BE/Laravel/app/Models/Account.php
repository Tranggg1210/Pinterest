<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Account extends Model
{
    protected $table = 'account';
    const CREATED_AT = 'token_created';
    const UPDATED_AT = 'token_updated';
    use HasFactory;
}
