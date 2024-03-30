<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;
use Illuminate\Support\Str;


class Account extends Model
{
    protected $table = 'account';
    protected $primaryKey  = 'user_id';
    const CREATED_AT = 'token_created';
    const UPDATED_AT = 'token_updated';
    use HasFactory;
}
