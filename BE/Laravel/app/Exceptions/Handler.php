<?php

namespace App\Exceptions;

use Exception;
use Illuminate\Foundation\Exceptions\Handler as ExceptionHandler;
use Throwable;
use Symfony\Component\HttpKernel\Exception\MethodNotAllowedHttpException;

class Handler extends ExceptionHandler
{
    /**
     * The list of the inputs that are never flashed to the session on validation exceptions.
     *
     * @var array<int, string>
     */
    protected $dontFlash = [
        'current_password',
        'password',
        'password_confirmation',
    ];

    /**
     * Register the exception handling callbacks for the application.
     */
    public function register(): void
    {
        $this->reportable(function (Throwable $e) {
            //
        });
    }
    public function __invoke()
    {
        return response() -> json([
            'status' => 'error',
           'message' => $e->getMessage(),
        ]);
    }
    public function render($request, Throwable $e)
    {
        if ($e instanceof Exception) {
            return redirect() -> route('not-found');
        }
        if ($e instanceof MethodNotAllowedHttpException) {
            return redirect() -> route('forbidden');
        }
    }
}
